using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using ICSharpCode.TextEditor;
using System.Reflection;
using System.Xml.Linq;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;

namespace XmlEditor
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        StreamReader sr;
        XDocument doc;
        int selstart = 0;
        Dictionary<string, List<object>> _litem = new Dictionary<string, List<object>>();
        List<object> listoftags;
        string _num;
        List<string> fileError = new List<string>();
        XDocument xdoc;

        string SpecialCharRegEX = ConfigurationManager.AppSettings["SpecialCharRegEX"];

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = xmlViewer1.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)xmlViewer1.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)xmlViewer1.Font.Size;
            }
            else
            {
                w = 50 + (int)xmlViewer1.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = xmlViewer1.GetCharIndexFromPosition(pt);
            int First_Line = xmlViewer1.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = xmlViewer1.GetCharIndexFromPosition(pt);
            int Last_Line = xmlViewer1.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        public Form1()
        {
            InitializeComponent();

            XMLViewerSetting viewerSetting = new XMLViewerSetting
            {
                AttributeKey = Color.Red,
                AttributeValue = Color.Blue,
                Tag = Color.Blue,
                Element = Color.DarkRed,
                Value = Color.Black,
            };
            xmlViewer1.Settings = viewerSetting;
        }


        private void openXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "XML Files (*.xml)|*.xml";
            ofd.FilterIndex = 0;
            ofd.DefaultExt = "xml";

            if (xmlViewer1.Text != "")
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ControlReset();
                    sr = new StreamReader(File.OpenRead(ofd.FileName));
                    label1.Text = ofd.SafeFileName;
                    var Dirpath = Environment.CurrentDirectory;
                    XmlSchemaSet schema = new XmlSchemaSet();
                    schema.Add("", Dirpath + "\\MTML_QUOTE_327-2019-ST0075-B(01)_SM0372_V10107_15081920541473.xsd");
           
                    XmlReaderSettings setting = new XmlReaderSettings { ValidationType = ValidationType.Schema };
                    setting.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                    setting.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                    setting.DtdProcessing = DtdProcessing.Parse;
                    setting.IgnoreWhitespace = true;
                    setting.IgnoreComments = true;

                    XmlUrlResolver resolver = new XmlUrlResolver();
                    resolver.Credentials = CredentialCache.DefaultCredentials;
                    setting.XmlResolver = resolver;
                    XmlReader rd = XmlReader.Create(sr, setting);

                    try
                    {
                        doc = XDocument.Load(rd);
                        doc.Validate(schema, ValidationEventHandler);
                        string x = doc.ToString();
                        string xml = RemoveSpecialCharacters(x);
                        xmlViewer1.Text = xml;
                        xmlViewer1.Process(true);
                        string _CurrentDir = Environment.CurrentDirectory;
                        string path = _CurrentDir + "\\UpdatedXML";
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        string xml_data = xml;
                        File.WriteAllText(path + "\\" + ofd.SafeFileName, xml_data);
                        FindDuplicate(xml);
                        LineNumberTextBox.Font = xmlViewer1.Font;
                        xmlViewer1.Select();
                        AddLineNumbers();
                    }
                    catch (XmlException xe)
                    {
                        string errstr = doc.ToString();
                        xmlViewer1.Text = errstr;
                        string temp = xe.Message;
                        HighlightError(temp);
                        string errorMessage = xe.Message;

                    }
                    #region Old Code for reading and display xml file
                    //  read = new StreamReader(File.OpenRead(ofd.FileName));

                    //  var filepath = File.OpenRead(ofd.FileName);
                    //  label1.Text = ofd.SafeFileName;
                    //  LineNumberTextBox.Font = xmlViewer1.Font;

                    //  //Reading all File content 
                    // // xmlViewer1.Text=x;
                    //  xmlViewer1.Text=doc.ToString();
                    // // xmlViewer1.Text = sr.ReadToEnd();
                    //  //xmlViewer1.Text = read.ReadToEnd();

                    //  AddLineNumbers();

                    //  // ValidateAgainstSchema(ofd.SafeFileName, schemaSet);
                    //  try
                    //  {
                    //      xmlViewer1.Process(true);
                    //  }
                    //  catch (ApplicationException appException)
                    //  {
                    //      //MessageBox.Show(appException.Message, "ApplicationException");
                    //      string temp = appException.Message;
                    //      HighlightError(temp);


                    //      richTextBox2.Clear();
                    //      string errorMessage = appException.Message;
                    //      richTextBox2.Text = appException.Message;

                    //      //xmlViewer1.Text = RemoveSpecialCharacters(xmlViewer1.Text);
                    //      //xmlViewer1.Process(true);
                    //    //  FindDuplicate(ofd.FileName);
                    //      richTextBox2.Clear();

                    //    //  findDuplicateItem();
                    //  }
                    //  catch (Exception ex)
                    //  {
                    //      MessageBox.Show(ex.Message, "Exception");
                    //  }
                    // // FindDuplicate(xmlViewer1.Text);
                    //  XmlTextReader reader = new XmlTextReader(filepath);
                    //  try
                    //  {
                    //      //xmlViewer1.Text = RemoveSpecialCharacters(xmlViewer1.Text);
                    //      //xmlViewer1.Process(true);
                    //  }
                    //  catch (Exception xe)
                    //  {
                    //      richTextBox2.Clear();
                    //      string errorMessage = xe.Message;
                    //      richTextBox2.Text = xe.Message;
                    //      string temp = xe.Message;
                    //      //HighlightError(temp);
                    //  }
                    ////  read.Close();
                    #endregion
                }
            }
            else if (ofd.ShowDialog() == DialogResult.OK)
            {

                sr = new StreamReader(File.OpenRead(ofd.FileName));
                label1.Text = ofd.SafeFileName;
                var Dirpath = Environment.CurrentDirectory;
                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add("", Dirpath + "\\MTML_QUOTE_327-2019-ST0075-B(01)_SM0372_V10107_15081920541473.xsd");
              
                XmlReaderSettings setting = new XmlReaderSettings { ValidationType = ValidationType.Schema };
                setting.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                setting.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                setting.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
                setting.DtdProcessing = DtdProcessing.Parse;
                setting.IgnoreProcessingInstructions = true;
                setting.IgnoreWhitespace = true;
                setting.IgnoreComments = true;

                XmlUrlResolver resolver = new XmlUrlResolver();
                resolver.Credentials = CredentialCache.DefaultCredentials;
                setting.XmlResolver = resolver;
                XmlReader rd = XmlReader.Create(sr, setting);

                try
                {
                    doc = XDocument.Load(rd);
                    doc.Validate(schema, ValidationEventHandler);
                    LineNumberTextBox.Font = xmlViewer1.Font;
                    xmlViewer1.Select();
                    AddLineNumbers();

                    string x = doc.ToString();
                    string xml1 = RemoveSpecialCharacters(x);               
                    xmlViewer1.Text = xml1;
                    xmlViewer1.Process(true);
                    string _CurrentDir = Environment.CurrentDirectory;  
                    string path = _CurrentDir + "\\UpdatedXML";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    string xml_data = xml1;
                    File.WriteAllText(path + "\\" + ofd.SafeFileName, xml_data);

                    FindDuplicate(xml1);
                }
                catch (ApplicationException appException)
                {
                    string errstr = doc.ToString();
                    xmlViewer1.Text = errstr;
                    string _err = appException.Message;
                    HighlightError(_err);
                    string errorMessage = _err;
                }
            }
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error) throw new Exception(e.Message);
            }
            Console.WriteLine("2");
        }

        #region RemoveSpecialCharacters
        public string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, SpecialCharRegEX ?? "", "", RegexOptions.Compiled);
        }
        #endregion

        #region #FindDuplicateTag
        public void FindDuplicate(string str)
        {
            xdoc = XDocument.Parse(str);
            

            var result = (from item in xdoc.Descendants()
                          where item.Elements("PriceDetails").Count(x => x.Attribute("TypeQualifier").Value == "GRP") > 1 && item.Element("PriceDetails").Attribute("TypeQualifier").Value == "GRP"
                          select item).ToList<XElement>();

            List<object> number = result.Attributes("Number").ToList<object>();

            List<object> _result = result.GroupBy(s => s.Value).SelectMany(grp => grp.Skip(1)).DescendantsAndSelf("PriceDetails").Where(x => x.Attribute("TypeQualifier").Value == "GRP").ToList<object>();

            for (int i = 0; i < result.Count; i++)
            {
                string n = "";
                n = n + number[i].ToString();
                _num = n.Split('=')[1];
                var x = result[i];
                listoftags = x.Descendants("PriceDetails").Where(z => z.Attribute("TypeQualifier").Value == "GRP").ToList<object>();
                _litem.Add(_num, listoftags);
            }

            HighLightDuplicateTagLine(_litem, str, number);
            _litem.Clear();
        }
        #endregion

        public void HighLightDuplicateTagLine(Dictionary<string, List<object>> _resDupTag, string _xmlFile, List<object> _num)
        {
            int firstCharIndex = 0;
            xmlViewer1.Text = _xmlFile;
            xmlViewer1.Process(true);
            int lastIndex = -1;
            int index = 0;
            var _val = "";
            string res = string.Empty;
            if (_resDupTag.Count > 0)
            {
                foreach (var x in _resDupTag)
                {
                    List<object> val = x.Value;
                    for (int i = 0; i < val.Count; i++)
                    {

                        _val = xmlViewer1.Lines.Where(s => s.Contains("Number=" + x.Key)).FirstOrDefault().ToString().Trim();

                        if (xmlViewer1.Text.Contains(_val))
                        {
                            index = xmlViewer1.Text.IndexOf(_val.ToString());
                        }

                        if (lastIndex != -1)
                        { firstCharIndex = xmlViewer1.Text.IndexOf(val[i].ToString(), lastIndex); }
                        else
                        {
                            firstCharIndex = xmlViewer1.Text.IndexOf(val[i].ToString(), index);
                        }
                        if (firstCharIndex > 0)
                        {
                            
                            int headerLine = xmlViewer1.GetLineFromCharIndex(firstCharIndex);
                            string headerLineText = xmlViewer1.Lines[headerLine].Trim();
                            if (i == 0)
                            { xmlViewer1.Select(firstCharIndex, headerLineText.Length + 12); }
                            else xmlViewer1.Select(lastIndex, headerLineText.Length + 12);
                            if (xmlViewer1.SelectionBackColor != System.Drawing.Color.Black)
                            {
                                xmlViewer1.SelectionColor = Color.Yellow;
                                xmlViewer1.SelectionBackColor = System.Drawing.Color.Black;
                            }
                            lastIndex = firstCharIndex + headerLineText.Length + 1;
                        }
                    }

                    _val = "";
                }
            }
        }

        #region HighlightErrorInXml
        public void HighlightError(string temp)
        {
            if (xmlViewer1.Text != "")
            {
                string match = Regex.Match(temp, @"[lL]ine \d+").Value;
                int LineNumber = Convert.ToInt32(Regex.Match(match, @"\d+").Value);
                if (!string.IsNullOrWhiteSpace(match))
                {
                    var array = xmlViewer1.Lines;
                    string cnt = array[LineNumber - 1];

                    string finalvalue = cnt;

                    string currentlinetext = xmlViewer1.Lines[LineNumber - 1];
                    int chr = xmlViewer1.GetFirstCharIndexFromLine(LineNumber - 1);

                    xmlViewer1.Select(chr, finalvalue.Length);
                    xmlViewer1.SelectionBackColor = Color.Yellow;
                }
            }
        }
        #endregion

        #region Reseting the contents in control
        private void ControlReset()
        {
            xmlViewer1.Clear();
        }
        #endregion

        #region Form1_Resize
        private void Form1_Resize(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Form1_Load
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region LineNumberTextBox_MouseDown
        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            xmlViewer1.Select();
            LineNumberTextBox.DeselectAll();
        }
        #endregion

        #region xmlViewer1_FontChanged
        private void xmlViewer1_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = xmlViewer1.Font;
            xmlViewer1.Select();
            AddLineNumbers();
        }
        #endregion

        #region xmlViewer1_VScroll
        private void xmlViewer1_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }
        #endregion

        #region UpdateXmlFile
        private void updateXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xmlViewer1.Text != "")
            {
                string _CurrentDir = Environment.CurrentDirectory;
                string path = _CurrentDir + "\\UpdatedXML";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string xml_data = xmlViewer1.Text;
                File.WriteAllText(path + "\\" + ofd.SafeFileName, xml_data);

                try
                {
                    xmlViewer1.Process(true);
                    MessageBox.Show(ofd.SafeFileName + " updated Successfully...");
                }
                catch (ApplicationException appException)
                {
                    xmlViewer1.SelectionBackColor = Color.White;
                    xmlViewer1.SelectionStart = xmlViewer1.GetFirstCharIndexFromLine(selstart);
                    MessageBox.Show(appException.Message, "ApplicationException");
                    string temp = appException.Message;
                    HighlightError(temp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception");
                }
            }
            else
            {
                MessageBox.Show("Missing To Load The file ");
            }
        }
        #endregion

        private void xmlViewer1_SelectionChanged(object sender, EventArgs e)
        {}

       
        private void xmlViewer1_TextChanged_1(object sender, EventArgs e)
        {}

        private void xmlViewer1_KeyDown(object sender, KeyEventArgs e)
        {
            if (xmlViewer1.Text != null && (e.Control && e.KeyCode == Keys.F))
            {
                string ImgPath = Environment.CurrentDirectory;
                this.textBox1.Visible = true;
                this.label2.Visible = true;
                this.label3.Visible = true;
            }
            else
            {
                this.textBox1.Visible = false;
                this.label2.Visible = false;
                this.label3.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int start = 0;
                int end = xmlViewer1.Text.LastIndexOf(textBox1.Text);

                xmlViewer1.SelectAll();
                xmlViewer1.SelectionBackColor = Color.White;

                while (start < end)
                {
                    xmlViewer1.Find(textBox1.Text, start, xmlViewer1.TextLength, RichTextBoxFinds.MatchCase);

                    xmlViewer1.SelectionBackColor = Color.Yellow;

                    start = xmlViewer1.Text.IndexOf(textBox1.Text, start) + 1;
                }
            }
            else
            {
                xmlViewer1.SelectAll();
                xmlViewer1.SelectionBackColor = Color.White;
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.label2.Visible = false;
            this.textBox1.Visible = false;
            this.label3.Visible = false;
            this.textBox1.Text = "";
        }
    }
}


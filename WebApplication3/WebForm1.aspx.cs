using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;


namespace WebApplication3
{
    public class Sentence
    {
        private int entries;
        private string words;
        public Sentence()
        {
            entries = 0;
            words = "";
        }
        public Sentence(int e, string w)
        {
            entries = e;
            words = w;
        }
        public int Entries
        {
            get { return entries; }
        }
        public string Words
        {
            get { return words; }
        }
    }

        public partial class WebForm1 : System.Web.UI.Page
        {
            public List<Sentence> calculateSentences()
            {
                List<Sentence> sentencesStore = new List<Sentence>();
                int entries = 0;
                bool toAdd = false;
                string str = null;
                if (fname != null)
                    str = File.ReadAllText(Server.MapPath("~/Files/" + fname));
                if (str != null)
                {
                    string[] splitedStr = str.ToString().Split('.');
                    for (int i = 0; i < splitedStr.Length; i++)
                    {
                        string[] spaceSplitted = splitedStr[i].Split(' ');
                        for (int j = 0; j < spaceSplitted.Length; j++)
                        {
                            if (spaceSplitted[j] == TextSearch.Text && TextSearch.Text != "")
                            {
                                toAdd = true;
                                entries++;
                            }
                        }
                        if (toAdd)
                        {

                            ForSentences.Text += splitedStr[i] + " " + entries + "<br>";
                            char[] arr = splitedStr[i].ToCharArray();
                            Array.Reverse(arr);
                            string reversed = new string(arr);
                            sentencesStore.Add(new Sentence(entries, reversed));
                            toAdd = false;
                            entries = 0;

                        }

                    }
                }

                return sentencesStore;
            }

            protected void Page_Load(object sender, EventArgs e)
            { }

            public static string fname = null;

            protected void searchButton_Click(object sender, EventArgs e)
            {

                //int entries = 0;

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\polzar\Desktop\Sentence Parser\WebApplication3\App_Data\Sentences.mdf;Integrated Security=True";//Path to Database
                string queryString =
                "insert into Sentences(Amount_of_entries, Sentence) values(@Amount_of_entries, @Sentence)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.Add("@Amount_of_entries", SqlDbType.Int);
                    command.Parameters.Add("@Sentence", SqlDbType.VarChar);
                    List<Sentence> mySentences = calculateSentences();
                    foreach (Sentence sentence in mySentences)
                    {
                        command.Parameters["@Amount_of_entries"].Value = sentence.Entries;
                        command.Parameters["@Sentence"].Value = sentence.Words;
                    command.ExecuteNonQuery();
                    }
                }
            }

            protected void uploadButton_Click(object sender, EventArgs e)
            {
                this.lbloutput.Text += this.TextSearch.Text;
                if (fileSentences.HasFile)
                {
                    fname = fileSentences.PostedFile.FileName;

                    string extension = Path.GetExtension(fname);
                    int flag = 0;
                    switch (extension.ToLower())
                    {
                        case ".doc":
                        case ".docx":
                        case ".txt":
                            flag = 1;
                            break;
                        default:
                            flag = 0;
                            break;

                    }
                    if (flag == 1)
                    {
                        int fileSize = fileSentences.PostedFile.ContentLength;
                        if (fileSize > 2097152)
                        {
                            lbloutput.Text = "Maximum file size (2MB). Choose ligther one!";
                        }
                        else
                        {
                            fileSentences.SaveAs(Server.MapPath("~/Files/" + fname));
                            lbloutput.Text = "File Uploaded";
                        }
                    }
                    else
                    {
                        lbloutput.Text = "Only .doc. docx .txt is Allowed";
                    }
                }
                else
                    lbloutput.Text = "Choose file to upload";

            }
            protected void TextSearch_TextChanged(object sender, EventArgs e)
            {

            }

        }
    }

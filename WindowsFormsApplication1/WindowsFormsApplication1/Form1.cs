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
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string[] martial = new string[] { "married", "single", "divorced" };
            string[] education = new string[] { "tertiray", "primary", "secondary", "unknown" };
            string[] loan = new string[] { "yes", "no" };
            string[] housing = new string[] { "yes", "no" };
            string[] contact = new string[] { "cellular", "telephone","unknown" };
            string[] month = new string[] { "jan", "feb", "mar","apr","may", "jun","jul","aug","sep","nov","dec" };

            foreach (string s in martial) comboBox1.Items.Add(s);
            comboBox1.SelectedIndex = -1;

            foreach (string s in education) comboBox2.Items.Add(s);
            comboBox2.SelectedIndex = -1;

            foreach (string s in loan) comboBox4.Items.Add(s);
            comboBox4.SelectedIndex = -1;

            foreach (string s in housing) comboBox3.Items.Add(s);
            comboBox3.SelectedIndex = -1;

            foreach (string s in contact) comboBox5.Items.Add(s);
            comboBox5.SelectedIndex = -1;

            foreach (string s in month) comboBox6.Items.Add(s);
            comboBox6.SelectedIndex = -1;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {



        }



        public class StringTable
        {

            public string[] ColumnNames { get; set; }

            public string[,] Values { get; set; }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string martial, education, loan, housing, age, job, balance, month, duration,result,contact;

            martial = comboBox1.Text;
            education = comboBox2.Text;
            loan = comboBox4.Text;
            housing = comboBox3.Text;
            age = textBox1.Text;
            job = textBox2.Text;
            balance = textBox3.Text;
            month = comboBox6.Text;
            duration = textBox5.Text;
            contact = comboBox5.Text;

            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() { 
                        { 
                            "input1", 
                            new StringTable() 
                            {
                                ColumnNames = new string[] {"age", "job", "marital", "education", "default", "balance", "housing", "loan", "contact", "day", "month", "duration", "campaign", "pdays", "previous", "poutcome", "y"},
                                Values = new string[,] {  { age, job, martial, education ,"no", balance, housing, loan, contact,"5", month, duration,"1","-1",  "0","unknown", "yes" }  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "EPWw4rcU/Xgv/3PfOcWEV/CuW4PqiJrzC3Rx6p+oicpFS2x9Ry/1NAzGboALhE7pcaFb1s8TeoAfAr/5S+OfhA=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/fb3896a249304b0ba9df71a9233d1673/services/08dd7414383f44bb997bf038d9955be5/execute?api-version=2.0&details=true");


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    
                }
                else
                {
                    result = await response.Content.ReadAsStringAsync();
                }

                result = transform(result);


                if (result == "n")
                {
                    label11.Text = "NE PRIHVAĆA";
                }

                if (result == "y")
                {
                    label11.Text = "PRIHVAĆA";
                }
                
            }

        }

        public string transform(string result)
        {

            string rezultat = "", b = "";

            int a = 0;

            for (int i = result.Length - 1; i > 0; i--)
            {



                if (result[i] == '"' && a == 1)
                {

                    a = 0;



                    break;

                }

                if (a == 1)
                {

                    b += result[i];



                }

                if (result[i] == '"' && a == 0)
                {

                    a = 1;



                }



            }





            for (int i = b.Length - 1; i > 0; i--)
            {

                rezultat += b[i];



            }



            return rezultat;

        }
    }
}

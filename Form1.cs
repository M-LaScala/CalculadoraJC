using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraJurosCompostos2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            float valorInicial = 0, valorMensal = 0, taxaDeJuros = 0, tempoInvestido = 0;

            try
            {
                 valorInicial = float.Parse(textBox1.Text);
                 valorMensal = float.Parse(textBox2.Text);
                 tempoInvestido = float.Parse(textBox3.Text);
                 taxaDeJuros = float.Parse(textBox4.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            float jurosSem = taxaDeJuros / 100;
            float valorFinal = 0;
            float rendimentos = 0;
            float valorInvestido = valorInicial + (valorMensal * tempoInvestido);

            #region Parte logica com as formulas
            if (valorInicial > 0 && valorMensal <= 0)
            {
                // Calcular rendimento sem depósitos mensais
                // Montante = CapitalInicial x ( (1 + jurosSem) ^ tempo )
                valorFinal = (float)(valorInicial * (Math.Pow((1 + jurosSem), tempoInvestido)));
                rendimentos = valorFinal - valorInicial;

            }
            else if (valorInicial <= 0 && valorMensal > 0)
            {
                // Calcular rendimento com somente depósitos mensais
                // Montante = Aportes × ( ( (1 + jurosSem) ^ tempo − 1) ÷ jurosSem );
                valorFinal = (float)(valorMensal * (Math.Pow((1 + jurosSem), tempoInvestido) - 1) / jurosSem);
                rendimentos = valorFinal - (valorMensal * tempoInvestido);

            }
            else if (valorInicial > 0 && valorMensal > 0)
            {
                // Calcular rendimento com depósito inicial, mais depósitos mensais 
                // Montante = (CapitalInicial x ((1 + jurosSem) ^ tempo)) + (valorMensal × (((((1 + jurosSem)) ^ tempo ) − 1) ÷ jurosSem)).
                valorFinal = ((float)((valorInicial * (Math.Pow((1 + jurosSem), tempoInvestido))) + ((valorMensal * (Math.Pow((1 + jurosSem), tempoInvestido) - 1)) / jurosSem)));
                rendimentos = valorFinal - ((valorMensal * tempoInvestido) + valorInicial);

            }
            else
            {
                // Não é póssivel calcular o rendimento
                Console.WriteLine("Erro ao calcular o rendimento!");
            }
            #endregion

            textBox5.Text = $"{valorFinal.ToString("C", CultureInfo.CurrentCulture)}";
            textBox6.Text = $"{rendimentos.ToString("C", CultureInfo.CurrentCulture)}";
            textBox7.Text = $"{valorInvestido.ToString("C", CultureInfo.CurrentCulture)}";
        }
    }
}

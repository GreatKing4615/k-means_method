using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Claster
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        Color[] arrayColor ;//цвета каждого центра
        Color[] default_arrayColor=new Color[10];//центры
        int[,] lastgrid2value;//значение последних координат цетров

        public Form1()
        {
            InitializeComponent();      
        }

        private void RefreshPaint()
        {
            int k;
            chart1.Series[1].Points.Clear();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                k = Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
                chart1.Series[1].MarkerStyle = MarkerStyle.Square;
                chart1.Series[1].ChartType = SeriesChartType.Point;
                chart1.Series[1].MarkerSize = 20;
                chart1.Series[1].Points.AddXY(Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value), Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value));
                chart1.Series[1].Points[i].Color = arrayColor[k];
            }
        }

        private void Paint()
        {
            default_arrayColor[0] = Color.Blue;
            default_arrayColor[1] = Color.Chocolate;
            default_arrayColor[2] = Color.Coral;
            default_arrayColor[3] = Color.DarkOrange;
            default_arrayColor[4] = Color.DeepPink;
            default_arrayColor[5] = Color.Tan;
            default_arrayColor[6] = Color.Firebrick;
            default_arrayColor[7] = Color.Green;
            default_arrayColor[8] = Color.Violet;
            default_arrayColor[9] = Color.Red;

            arrayColor = new Color[Convert.ToInt32(textBox1.Text)];

            if (Convert.ToInt32(textBox1.Text) >= 10)
            {
                for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                {
                    arrayColor[i] = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                }
            }
            else
            {
                int[] contin = new int[10];
                int q = 0;
                for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                {
                    q = rnd.Next(0, 9);
                    if (contin[q] != 1)
                    {
                        arrayColor[i] = default_arrayColor[q];
                        contin[q] = 1;
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            RefreshPaint();
        }

        /// <summary>
        /// Перерасчет координат центроидов
        /// </summary>
        private void Centr_Masss() {
            int averageX;
            int averageY;
            int count;
            try
            {
                for (int j = 0; j < dataGridView2.RowCount; j++)
                {
                    averageX = 0;
                    averageY = 0;
                    count = 0;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {

                        if (dataGridView1.Rows[i].Cells[3].Value.ToString() == j.ToString())
                        {
                            averageX += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                            averageY += Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                            count++;
                        }
                    }
                    dataGridView2.Rows[j].Cells[1].Value = Convert.ToString(averageX / count);
                    dataGridView2.Rows[j].Cells[2].Value = Convert.ToString(averageY / count);
                    dataGridView2.Rows[j].Cells[4].Value = Convert.ToString(count);
                }
                RefreshPaint();
            }
            catch { }
        }

        /// <summary>
        /// отображение точек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            DataPoint dp = new DataPoint();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                chart1.Series[0].MarkerStyle = MarkerStyle.Circle;               
                chart1.Series[0].ChartType = SeriesChartType.Point;
                chart1.Series[0].MarkerSize = 10;
                chart1.Series[0].Points.AddXY(Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value), Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value));             
            }
            Paint();
        }
        //лень удалять
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {         
        }
        //аналогично
        private void chart1_Click(object sender, EventArgs e)
        {            
        }

        /// <summary>
        /// заполнение случайными координатами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //foreach(int i in dataGridView1.Rows)
            dataGridView1.Rows.Clear();
            dataGridView1.RowCount = 0;
            dataGridView2.Rows.Clear();
            dataGridView2.RowCount = 0;
            int N = Convert.ToInt32(textBox2.Text)-1;
            for (int i = 0; i <=N; i++)
            {
                dataGridView1.Rows.Add(i, rnd.Next(1000), rnd.Next(1000),"-");
            }

            for(int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                dataGridView2.Rows.Add(i, rnd.Next(1000), rnd.Next(1000), i);
            }
            button1.PerformClick();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int k = e.RowIndex;
                chart1.Series[0].Points[k].MarkerSize = 20;
                chart1.Series[1].Points[k].BorderColor = Color.Black;
            }
            catch {             
            }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int k = e.RowIndex;
                chart1.Series[0].Points[k].MarkerSize = 7;
                chart1.Series[1].Points[k].BorderWidth=0;
            }
            catch
            {

            }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int k = e.RowIndex;
                chart1.Series[1].Points[k].MarkerSize = 40;
                chart1.Series[1].Points[k].BorderColor = Color.Black;
                chart1.Series[1].Points[k].BorderWidth = 10;
            }
            catch
            {
            }
        }

        private void dataGridView2_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int k = e.RowIndex;
                chart1.Series[1].Points[k].MarkerSize = 20;
                chart1.Series[1].Points[k].BorderColor = Color.Empty;
                chart1.Series[1].Points[k].BorderWidth = 0;
            }
            catch
            {
            }
        }


        private double distance(int i1, int i2)//задаю номерами точки
        {
            double d = 0;
            d += (Convert.ToDouble(dataGridView2.Rows[i2].Cells[1].Value) - Convert.ToDouble(dataGridView1.Rows[i1].Cells[1].Value)) * (Convert.ToDouble(dataGridView2.Rows[i2].Cells[1].Value) - Convert.ToDouble(dataGridView1.Rows[i1].Cells[1].Value));
            d += (Convert.ToDouble(dataGridView2.Rows[i2].Cells[2].Value) - Convert.ToDouble(dataGridView1.Rows[i1].Cells[2].Value)) * (Convert.ToDouble(dataGridView2.Rows[i2].Cells[2].Value) - Convert.ToDouble(dataGridView1.Rows[i1].Cells[2].Value));
            return (Math.Sqrt(d));
        }

        /// <summary>
        /// запуск алгоритма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0) return;
            lastgrid2value = new int[2,Convert.ToInt32(textBox2.Text)];            
            bool flag = false;
            while (flag!=true) {
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    lastgrid2value[0, i] = Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
                    lastgrid2value[1, i] = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
                }
                double[] g = new double[2];                
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    g[0] = double.MaxValue;
                    g[1] = double.MaxValue;
                    for (int j = 0; j < dataGridView2.RowCount; j++)
                    {
                        if (g[0] >= distance(i, j))
                        {
                            g[0] = distance(i, j);
                            g[1] = j;
                        }
                    }
                    if (g[1] == double.MaxValue) continue;
                    dataGridView1.Rows[i].Cells[3].Value = Convert.ToString(g[1]);
                    chart1.Series[0].Points[i].Color = chart1.Series[1].Points[Convert.ToInt32(g[1])].Color;
                    dataGridView1.Rows[i].Cells[3].Value = Convert.ToString(g[1]);
                }
                
                Centr_Masss();
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    if (lastgrid2value[0,i] == Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value) && lastgrid2value[1,i] == Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value))
                    {
                        flag = true;
                        MessageBox.Show(Convert.ToString("finish"));
                        break;
                    }
                    
                }
            }            
        }
        /// <summary>
        /// отчистка 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            button1.PerformClick();
        }
    }
}

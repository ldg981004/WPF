using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearningProject.Projects.이동길
{
    /// <summary>
    /// new_answer_board.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class new_answer_board : UserControl
    {
        public new_answer_board()
        {
            InitializeComponent();

            Grid a_grid = new Grid();
            a_grid.Width = 359;
            a_grid.Height = 360;
            a_grid.ShowGridLines = false;

            ColumnDefinition cd1 = new ColumnDefinition();
            ColumnDefinition cd2 = new ColumnDefinition();
            ColumnDefinition cd3 = new ColumnDefinition();
            ColumnDefinition cd4 = new ColumnDefinition();
            ColumnDefinition cd5 = new ColumnDefinition();

            a_grid.ColumnDefinitions.Add(cd1);
            a_grid.ColumnDefinitions.Add(cd2);
            a_grid.ColumnDefinitions.Add(cd3);
            a_grid.ColumnDefinitions.Add(cd4);
            a_grid.ColumnDefinitions.Add(cd5);

            RowDefinition rd1 = new RowDefinition();
            RowDefinition rd2 = new RowDefinition();
            RowDefinition rd3 = new RowDefinition();
            RowDefinition rd4 = new RowDefinition();
            RowDefinition rd5 = new RowDefinition();

            a_grid.RowDefinitions.Add(rd1);
            a_grid.RowDefinitions.Add(rd2);
            a_grid.RowDefinitions.Add(rd3);
            a_grid.RowDefinitions.Add(rd4);
            a_grid.RowDefinitions.Add(rd5);


            using (Entities8 ei = new Entities8())
            {
                int ro;
                int co;
                Dictionary<string, string> dic = new Dictionary < string, string>();


                var a = ei.PUZZLE.Where(u => u.NUM == "1").ToList();
                //임산부 세로문제
                foreach (var item in a)
                {

                    ro = int.Parse(item.RO);
                    co = int.Parse(item.CO);

                    for (int i = 0; i < item.ANS.Length; i++)
                    {
                        TextBox tb1 = new TextBox();
                        tb1.HorizontalContentAlignment = HorizontalAlignment.Center;
                        tb1.VerticalContentAlignment = VerticalAlignment.Center;
                        tb1.FontSize = 23;
                        //tb1.Text = "임산부";
                        Grid.SetRow(tb1, ro + i);
                        Grid.SetColumn(tb1, co);

                        dic.Add("TB1-"+i, (ro + i).ToString() +"|"+ co.ToString());
                        a_grid.Children.Add(tb1);
                    }
                    //foreach (KeyValuePair<string,string> pair in dic)
                    //{
                    //    Console.WriteLine(pair.Key +" : "+ pair.Value);
                    //}

                }

                var b = ei.PUZZLE.Where(u => u.NUM == "2" && u.POS == "가로").ToList();
                //잉꼬부부 가로문제
                foreach (var item in b)
                {
                    //int ro;
                    //int co;

                    ro = int.Parse(item.RO);
                    //2행
                    co = int.Parse(item.CO);
                    //1열

                    for (int i = 0; i < item.ANS.Length; i++)
                    {
                        TextBox tb2 = new TextBox();
                        tb2.HorizontalContentAlignment = HorizontalAlignment.Center;
                        tb2.VerticalContentAlignment = VerticalAlignment.Center;
                        tb2.FontSize = 23;
                        //tb2.Text = "TB2-" + i;

                        if (dic.ContainsValue(ro.ToString() + "|" + (co + i).ToString())){
                            continue;
                        }
                        else
                        {
                            Grid.SetRow(tb2, ro);
                            Grid.SetColumn(tb2, co + i);
                            dic.Add("TB2-" + i, ro.ToString() + "|" + (co + i).ToString());
                            a_grid.Children.Add(tb2);
                        }
                    }
                    //foreach (KeyValuePair<string, string> pair in dic)
                    //{
                    //    Console.WriteLine(pair.Key + " : " + pair.Value);
                    //}
                }

                var c = ei.PUZZLE.Where(u => u.ANS == "잉크병").ToList();
                //잉크병 세로문제
                foreach (var item in c)
                {
                    //int ro;
                    //int co;

                    ro = int.Parse(item.RO);
                    // 2행
                    co = int.Parse(item.CO);
                    // 1열

                    for (int i = 0; i < item.ANS.Length; i++)
                    {
                        TextBox tb3 = new TextBox();
                        tb3.HorizontalContentAlignment = HorizontalAlignment.Center;
                        tb3.VerticalContentAlignment = VerticalAlignment.Center;
                        tb3.FontSize = 23;
                        //tb3.Text = "TB3-" + i;

                        if (dic.ContainsValue((ro + i).ToString() + "|" + co.ToString()))
                        {
                            //Console.WriteLine("value가 있음");
                            continue;
                        }
                        else
                        {
                            Grid.SetRow(tb3, ro + i);
                            Grid.SetColumn(tb3, co);
                            dic.Add("TB3-" + i, (ro + i).ToString() + "|" + co.ToString());
                            a_grid.Children.Add(tb3);
                        }
                    }
                    //foreach (KeyValuePair<string, string> pair in dic)
                    //{
                    //    Console.WriteLine(pair.Key + " : " + pair.Value);
                    //}
                }

                var d = ei.PUZZLE.Where(u => u.NUM == "3" && u.POS == "세로").ToList();
                // 부금 세로문제
                foreach (var item in d)
                {
                    //int ro;
                    //int co;

                    ro = int.Parse(item.RO);
                    co = int.Parse(item.CO);

                    for (int i = 0; i < item.ANS.Length; i++)
                    {
                        TextBox tb4 = new TextBox();
                        tb4.HorizontalContentAlignment = HorizontalAlignment.Center;
                        tb4.VerticalContentAlignment = VerticalAlignment.Center;
                        tb4.FontSize = 23;
                        //tb4.Text = "TB4-" + i;

                        if (dic.ContainsValue((ro + i).ToString() + "|" + co.ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            Grid.SetRow(tb4, ro + i);
                            Grid.SetColumn(tb4, co);
                            dic.Add("TB4-" + i, (ro + i).ToString() + "|" + co.ToString());
                            a_grid.Children.Add(tb4);
                        }
                    }
                }

                var e = ei.PUZZLE.Where(u => u.NUM == "4").ToList();
                foreach (var item in e)
                {
                    //int ro;
                    //int co;

                    ro = int.Parse(item.RO);
                    co = int.Parse(item.CO);

                    for (int i = 0; i < item.ANS.Length; i++)
                    {
                        TextBox tb5 = new TextBox();
                        tb5.HorizontalContentAlignment = HorizontalAlignment.Center;
                        tb5.VerticalContentAlignment = VerticalAlignment.Center;
                        tb5.FontSize = 23;
                        //tb5.Text = "TB5-" + i;

                        if (dic.ContainsValue(ro.ToString() + "|" + (co + i).ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            Grid.SetRow(tb5, ro);
                            Grid.SetColumn(tb5, co + i);

                            dic.Add("TB5-" + i, ro.ToString() + "|" + (co + i).ToString());
                            a_grid.Children.Add(tb5);
                        }
                    }
                }

                var f = ei.PUZZLE.Where(u => u.NUM == "5").ToList();
                foreach (var item in f)
                {
                    //int ro;
                    //int co;

                    ro = int.Parse(item.RO);
                    co = int.Parse(item.CO);

                    for (int i = 0; i < item.ANS.Length; i++)
                    {
                        TextBox tb6 = new TextBox();
                        tb6.HorizontalContentAlignment = HorizontalAlignment.Center;
                        tb6.VerticalContentAlignment = VerticalAlignment.Center;
                        tb6.FontSize = 23;
                        //tb6.Text = "TB6-" + i;

                        if (dic.ContainsValue(ro.ToString() + "|" + (co + i).ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            Grid.SetRow(tb6, ro);
                            Grid.SetColumn(tb6, co + i);

                            dic.Add("TB6-" + i, ro.ToString() + "|" + (co + i).ToString());
                            a_grid.Children.Add(tb6);
                        }
                    }
                    foreach (KeyValuePair<string, string> pair in dic)
                    {
                        Console.WriteLine(pair.Key + " : " + pair.Value);
                    }
                }
            }



            g.Children.Add(a_grid);
        }
    }
}

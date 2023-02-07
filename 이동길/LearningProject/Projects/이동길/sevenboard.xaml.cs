using System;
using System.Collections.Generic;
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
    /// sevenboard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class sevenboard : UserControl
    {
        int count = 0;

        List<TextBox> lt = new List<TextBox>();
        List<TextBox> lt2 = new List<TextBox>();

        public sevenboard()
        {
            InitializeComponent();
            this.Loaded += Sevenboard_Loaded;
        }

        private void Sevenboard_Loaded(object sender, RoutedEventArgs e)
        {
            definition();

            Getitem();

            plus_textbox();
        }

        private void definition()
        {
            RowDefinition rd1 = new RowDefinition();
            RowDefinition rd2 = new RowDefinition();
            RowDefinition rd3 = new RowDefinition();
            RowDefinition rd4 = new RowDefinition();
            RowDefinition rd5 = new RowDefinition();
            RowDefinition rd6 = new RowDefinition();
            RowDefinition rd7 = new RowDefinition();

            grid_Answer2.RowDefinitions.Add(rd1);
            grid_Answer2.RowDefinitions.Add(rd2);
            grid_Answer2.RowDefinitions.Add(rd3);
            grid_Answer2.RowDefinitions.Add(rd4);
            grid_Answer2.RowDefinitions.Add(rd5);
            grid_Answer2.RowDefinitions.Add(rd6);
            grid_Answer2.RowDefinitions.Add(rd7);


            ColumnDefinition cd1 = new ColumnDefinition();
            ColumnDefinition cd2 = new ColumnDefinition();
            ColumnDefinition cd3 = new ColumnDefinition();
            ColumnDefinition cd4 = new ColumnDefinition();
            ColumnDefinition cd5 = new ColumnDefinition();
            ColumnDefinition cd6 = new ColumnDefinition();
            ColumnDefinition cd7 = new ColumnDefinition();

            grid_Answer2.ColumnDefinitions.Add(cd1);
            grid_Answer2.ColumnDefinitions.Add(cd2);
            grid_Answer2.ColumnDefinitions.Add(cd3);
            grid_Answer2.ColumnDefinitions.Add(cd4);
            grid_Answer2.ColumnDefinitions.Add(cd5);
            grid_Answer2.ColumnDefinitions.Add(cd6);
            grid_Answer2.ColumnDefinitions.Add(cd7);

            grid_Answer2.ShowGridLines = true;
        }

        private List<problem> Getitem()
        {
            List<problem> problems = new List<problem>();

            StackPanel sp = new StackPanel();
            ScrollViewer sv = new ScrollViewer();
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            sp.Orientation = Orientation.Vertical;
            sv.Content = sp;
            question.Children.Add(sv);

            using (Entities8 ei = new Entities8())
            {
                string que1 = "가로";
                string que2 = "세로";

                var q = from e in ei.PUZZLE
                        where e.SEQ.Contains("3")
                        select e;

                foreach (var a in q)
                {
                    problems.Add(new problem()
                    {
                        pro_num = a.NUM,
                        pro_que = a.QUE,
                        pro_ans = a.ANS,
                        pro_pos = a.POS,
                        pro_co = a.CO,
                        pro_ro = a.RO,
                        pro_seq = a.SEQ

                    });
                }
                Label lab = new Label();
                lab.Content = "가로 문제";
                lab.FontSize = 15;
                sp.Children.Add(lab);
                
                for (int i = 0; i < problems.Count; i++)
                {
                    if (problems[i].pro_pos == que1)
                    {
                        TextBlock txb = new TextBlock();
                        txb.Margin = new Thickness(5, 5, 5, 15);
                        txb.TextWrapping = TextWrapping.WrapWithOverflow;
                        txb.Text = problems[i].pro_num + " " + problems[i].pro_que;
                        sp.Children.Add(txb);
                    }
                }

                Label lab2 = new Label();
                lab2.Content = "세로 문제";
                lab2.FontSize = 15;
                sp.Children.Add(lab2);
                for (int i = 0; i < problems.Count; i++)
                {
                    if(problems[i].pro_pos == que2)
                    {
                        TextBlock txb = new TextBlock();
                        txb.Margin = new Thickness(5, 5, 5, 15);
                        txb.TextWrapping = TextWrapping.WrapWithOverflow;
                        txb.Text = problems[i].pro_num + " " + problems[i].pro_que;
                        sp.Children.Add(txb);
                    }
                }
            }
            return problems;
        }

        private List<problem> Getitem2()
        {
            List<problem> problems = new List<problem>();
            StackPanel sp = new StackPanel();
            ScrollViewer sv = new ScrollViewer();
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            sp.Orientation = Orientation.Vertical;
            sv.Content = sp;
            question.Children.Add(sv);

            using (Entities8 ei = new Entities8())
            {
                string que1 = "가로";
                string que2 = "세로";

                var q = from e in ei.PUZZLE
                        where e.SEQ.Contains("4")
                        select e;

                foreach (var a in q)
                {
                    problems.Add(new problem()
                    {
                        pro_num = a.NUM,
                        pro_que = a.QUE,
                        pro_ans = a.ANS,
                        pro_pos = a.POS,
                        pro_co = a.CO,
                        pro_ro = a.RO,
                        pro_seq = a.SEQ

                    });
                }
                Label lab = new Label();
                lab.Content = "가로 문제";
                lab.FontSize = 15;
                sp.Children.Add(lab);
                
                for (int i = 0; i < problems.Count; i++)
                {
                    if (problems[i].pro_pos == que1)
                    {
                        TextBlock txb = new TextBlock();
                        txb.Margin = new Thickness(5, 5, 5, 15);
                        txb.TextWrapping = TextWrapping.WrapWithOverflow;
                        txb.Text = problems[i].pro_num + " " + problems[i].pro_que;
                        sp.Children.Add(txb);
                    }
                }

                Label lab2 = new Label();
                lab2.Content = "세로 문제";
                lab2.FontSize = 15;
                sp.Children.Add(lab2);
                for (int i = 0; i < problems.Count; i++)
                {
                    if (problems[i].pro_pos == que2)
                    {
                        TextBlock txb = new TextBlock();
                        txb.Margin = new Thickness(5, 5, 5, 15);
                        txb.TextWrapping = TextWrapping.WrapWithOverflow;
                        txb.Text = problems[i].pro_num + " " + problems[i].pro_que;
                        sp.Children.Add(txb);
                    }
                }
            }

            return problems;

        }

        private void plus_textbox()
        {
            using (Entities8 ei = new Entities8())
            {
                int ro;
                int co;
                List<string> answer = new List<string>();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                //가로 답안
                var z = ei.PUZZLE.Where(a => a.SEQ == "3").ToList();

                for (int i = 0; i < z.Count; i++)
                {
                    answer.Add(z[i].ANS);
                }

                for (int i = 0; i < answer.Count; i++)
                {
                    var w = answer[i].ToString();

                    var k = ei.PUZZLE.Where(u => u.ANS == w);

                    foreach (var item in k)
                    {
                        ro = int.Parse(item.RO);
                        co = int.Parse(item.CO);

                        for(int j = 0; j < item.ANS.Length; j++)
                        {
                            TextBox tb = new TextBox();
                            tb.MaxLength = 1;
                            tb.HorizontalContentAlignment = HorizontalAlignment.Center;
                            tb.VerticalContentAlignment = VerticalAlignment.Center;
                            tb.FontSize = 23;
                            

                            if (item.POS.ToString() == "가로")
                            {
                                if (dic.ContainsValue(ro.ToString() + "|" + (co + j).ToString()))
                                {
                                    continue;
                                }
                                else
                                {
                                    Grid.SetRow(tb, ro);
                                    Grid.SetColumn(tb, co + j);
                                    lt.Add(tb);
                                    dic.Add("TBC-" +i.ToString() + "-" + j.ToString() , ro.ToString() + "|" + (co + j).ToString());
                                    grid_Answer2.Children.Add(tb);
                                }
                            }
                            else
                            // POS == "세로"
                            {
                                if (dic.ContainsValue((ro + j).ToString() + "|" + co.ToString()))
                                {
                                    continue;
                                }
                                else
                                {
                                    Grid.SetRow(tb, ro + j);
                                    Grid.SetColumn(tb, co);
                                    lt.Add(tb);
                                    dic.Add("TBR-"+ i.ToString() + "-" + j.ToString(), (ro + j).ToString() + "|" + co.ToString());
                                    grid_Answer2.Children.Add(tb);
                                }
                            }
                        }
                    }
                }  
            }
        }

        private void plus_textbox2()
        {
            using (Entities8 ei = new Entities8())
            {
                int ro;
                int co;
                List<string> answer2 = new List<string>();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                var z = ei.PUZZLE.Where(a => a.SEQ == "4").ToList();

                for (int i = 0; i < z.Count; i++)
                {
                    answer2.Add(z[i].ANS);
                }

                for (int i = 0; i < answer2.Count; i++)
                {
                    var w = answer2[i].ToString();

                    var k = ei.PUZZLE.Where(u => u.ANS == w);

                    foreach (var item in k)
                    {
                        ro = int.Parse(item.RO);
                        co = int.Parse(item.CO);

                        for (int j = 0; j < item.ANS.Length; j++)
                        {
                            TextBox tb = new TextBox();
                            tb.MaxLength = 1;
                            tb.HorizontalContentAlignment = HorizontalAlignment.Center;
                            tb.VerticalContentAlignment = VerticalAlignment.Center;
                            tb.FontSize = 23;
                            //tb.Text = "answer" + i.ToString() + j.ToString();

                            if (item.POS.ToString() == "가로")
                            {
                                if (dic.ContainsValue(ro.ToString() + "|" + (co + j).ToString()))
                                {
                                    continue;
                                }
                                else
                                {
                                    Grid.SetRow(tb, ro);
                                    Grid.SetColumn(tb, co + j);
                                    lt2.Add(tb);
                                    dic.Add("TBC-" + i.ToString() + "-" + j.ToString(), ro.ToString() + "|" + (co + j).ToString());
                                    grid_Answer2.Children.Add(tb);
                                }
                            }
                            else
                            // POS == "세로"
                            {
                                if (dic.ContainsValue((ro + j).ToString() + "|" + co.ToString()))
                                {
                                    continue;
                                }
                                else
                                {
                                    Grid.SetRow(tb, ro + j);
                                    Grid.SetColumn(tb, co);
                                    lt2.Add(tb);
                                    dic.Add("TBR-" + i.ToString() + "-" + j.ToString(), (ro + j).ToString() + "|" + co.ToString());
                                    grid_Answer2.Children.Add(tb);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void score()
        {
            using (Entities8 ei = new Entities8())
            {
                List<string> word = new List<string>();

                if (lt.Count != 0)
                {
                    word.Add(lt[0].Text + lt[1].Text);
                    word.Add(lt[2].Text + lt[3].Text);
                    word.Add(lt[4].Text + lt[5].Text + lt[6].Text + lt[7].Text);
                    word.Add(lt[8].Text + lt[9].Text + lt[10].Text);
                    word.Add(lt[11].Text + lt[12].Text + lt[13].Text);
                    word.Add(lt[14].Text + lt[15].Text + lt[16].Text);

                    word.Add(lt[0].Text + lt[3].Text);
                    word.Add(lt[2].Text + lt[17].Text + lt[8].Text);
                    word.Add(lt[18].Text + lt[5].Text);
                    word.Add(lt[4].Text + lt[10].Text);
                    word.Add(lt[6].Text + lt[19].Text + lt[11].Text + lt[20].Text);
                    word.Add(lt[9].Text + lt[21].Text + lt[15].Text + lt[22].Text);
                }
                else if (lt2.Count != 0)
                {
                    word.Add(lt2[0].Text + lt2[1].Text);
                    word.Add(lt2[2].Text + lt2[3].Text);
                    word.Add(lt2[4].Text + lt2[5].Text + lt2[6].Text);
                    word.Add(lt2[7].Text + lt2[8].Text);
                    word.Add(lt2[9].Text + lt2[10].Text);
                    word.Add(lt2[11].Text + lt2[12].Text + lt2[13].Text);
                    word.Add(lt2[14].Text + lt2[15].Text + lt2[16].Text);

                    word.Add(lt2[1].Text + lt2[4].Text + lt2[8].Text);
                    word.Add(lt2[2].Text + lt2[6].Text);
                    word.Add(lt2[7].Text + lt2[17].Text + lt2[11].Text + lt2[16].Text);
                    word.Add(lt2[9].Text + lt2[13].Text + lt2[18].Text + lt2[19].Text);
                    word.Add(lt2[15].Text + lt2[20].Text);
                }

                else
                {
                    count = 0;
                }

                foreach (var item in word)
                {
                    var qu1 = from ae in ei.PUZZLE
                              where ae.ANS == item
                              select ae;

                    foreach (var k in qu1)
                    {
                        count = count + 1;
                    }
                }
                word.Clear();
            }
        }

        public class problem
        {
            public string pro_num { get; set; }
            public string pro_que { get; set; }
            public string pro_ans { get; set; }
            public string pro_pos { get; set; }
            public string pro_ro { get; set; }
            public string pro_co { get; set; }
            public string pro_seq { get; set; }
        }

        private void five_Selected(object sender, RoutedEventArgs e)
        {
            grid17_1.Children.Clear();
            grid17_1.RowDefinitions.Clear();
            grid17_1.ColumnDefinitions.Clear();
            grid17_1.Children.Add(new board());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            score();
            MessageBox.Show(count.ToString() + "개 정답", "채점 결과");
            count = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //새문제 버튼
            grid_Answer2.Children.Clear();
            question.Children.Clear();
            lt.Clear();
            Getitem2();
            plus_textbox2();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //다시풀기 버튼
            grid_Answer2.Children.Clear();
            question.Children.Clear();
            Getitem();
            lt.Clear();
            lt2.Clear();
            plus_textbox();
        }
    }
}

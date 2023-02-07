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
using System.Diagnostics;

namespace LearningProject.Projects.이동길
{
    /// <summary>
    /// board.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class board : UserControl
    {
  
        int count = 0;

        List<TextBox> lt = new List<TextBox>();
        List<TextBox> lt2 = new List<TextBox>();

        Stopwatch stopwatch = new Stopwatch();
        



        public board()
        {
            InitializeComponent();

            definition();

            Getitem();

            plus_textbox(); 
            
            stopwatch.Start();
        }
        

        private void definition()
        {
            ColumnDefinition cd1 = new ColumnDefinition();
            ColumnDefinition cd2 = new ColumnDefinition();
            ColumnDefinition cd3 = new ColumnDefinition();
            ColumnDefinition cd4 = new ColumnDefinition();
            ColumnDefinition cd5 = new ColumnDefinition();

            grid_Answer.ColumnDefinitions.Add(cd1);
            grid_Answer.ColumnDefinitions.Add(cd2);
            grid_Answer.ColumnDefinitions.Add(cd3);
            grid_Answer.ColumnDefinitions.Add(cd4);
            grid_Answer.ColumnDefinitions.Add(cd5);

            RowDefinition rd1 = new RowDefinition();
            RowDefinition rd2 = new RowDefinition();
            RowDefinition rd3 = new RowDefinition();
            RowDefinition rd4 = new RowDefinition();
            RowDefinition rd5 = new RowDefinition();

            grid_Answer.RowDefinitions.Add(rd1);
            grid_Answer.RowDefinitions.Add(rd2);
            grid_Answer.RowDefinitions.Add(rd3);
            grid_Answer.RowDefinitions.Add(rd4);
            grid_Answer.RowDefinitions.Add(rd5);

            grid_Answer.ShowGridLines = true;
        }


        private List<problem> Getitem()
        {
            List<problem> problems = new List<problem>();
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            question_board.Children.Add(sp);

            using (Entities8 ei = new Entities8())
            {
                string que1 = "가로";
                string que2 = "세로";

                var q = from e in ei.PUZZLE
                        where e.SEQ.Contains("1")
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
                        txb.Margin = new Thickness(5, 5, 5, 20);
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
                        txb.Margin = new Thickness(5, 5, 5, 20);
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
            sp.Orientation = Orientation.Vertical;
            question_board.Children.Add(sp);

            using (Entities8 ei = new Entities8())
            {
                string que1 = "가로";
                string que2 = "세로";

                var q = from e in ei.PUZZLE
                        where e.SEQ.Contains("2")
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
                        txb.Margin = new Thickness(5, 5, 5, 20);
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
                        txb.Margin = new Thickness(5, 5, 5, 20);
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

                var z = ei.PUZZLE.Where(a => a.SEQ == "1").ToList();

                for(int i = 0; i < z.Count; i++)
                {
                    answer.Add(z[i].ANS);
                }

                for (int i = 0; i < answer.Count; i++)
                {
                    var w = answer[i].ToString();

                    var k = ei.PUZZLE.Where(u => u.ANS == w);

                    foreach (var item in k)
                    {
                        ro = int.Parse(item.RO); //아이템의 ROW 값을 인트로 가져옴
                        co = int.Parse(item.CO); //아이템의 COL 값을 인트로 가져옴

                        for (int j = 0; j < item.ANS.Length; j++) // 아이템 길이만큼 반복
                        {
                            TextBox tb = new TextBox();
                            tb.MaxLength = 1;
                            tb.HorizontalContentAlignment = HorizontalAlignment.Center;
                            tb.VerticalContentAlignment = VerticalAlignment.Center;
                            tb.FontSize = 23;


                            if (item.POS.ToString() == "가로") // 모양이 가로인 친구이면
                            {
                                if (dic.ContainsValue(ro.ToString() + "|" + (co + j).ToString()))
                                    //택스트 박스 위치 딕셔너리에 값이 있으면 
                                {
                                    continue;
                                    // 다시 for문을 돌고
                                }
                                else
                                // 아니면
                                {
                                    Grid.SetRow(tb, ro); // 가로니깐 Row는 고정으로 배치
                                    Grid.SetColumn(tb, co + j); // 택스트 박스르 만들 때 옆으로 한칸씩 띄어서 만들어야 함
                                                                // Column을 배치할 때 1씩 더해서 택스트 박스를 배치
                                    lt.Add(tb);
                                    dic.Add("TBC-" + i.ToString() + "-" + j.ToString(), ro.ToString() + "|" + (co + j).ToString());
                                    grid_Answer.Children.Add(tb); //게임 보드판에 만들어진 택스트 박스의 ROW와 COL에 맞게 배치
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
                                    dic.Add("TBR-" + i.ToString() + "-" + j.ToString(), (ro + j).ToString() + "|" + co.ToString());
                                    grid_Answer.Children.Add(tb);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void plus_box2()
        {
            using (Entities8 ei = new Entities8())
            {
                int ro;
                int co;
                List<string> answer = new List<string>();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                var z = ei.PUZZLE.Where(a => a.SEQ == "2").ToList();

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

                        for (int j = 0; j < item.ANS.Length; j++)
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
                                    lt2.Add(tb);
                                    dic.Add("TBC-" + i.ToString() + "-" + j.ToString(), ro.ToString() + "|" + (co + j).ToString());
                                    grid_Answer.Children.Add(tb);
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
                                    grid_Answer.Children.Add(tb);
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
                    word.Add(lt[0].Text + lt[1].Text + lt[2].Text);
                    word.Add(lt[3].Text + lt[4].Text + lt[5].Text + lt[2].Text);
                    word.Add(lt[3].Text + lt[6].Text + lt[7].Text);
                    word.Add(lt[5].Text + lt[8].Text);
                    word.Add(lt[9].Text + lt[6].Text);
                    word.Add(lt[7].Text + lt[10].Text);
                  
                }

                else if (lt2.Count != 0)
                {
                    word.Add(lt2[0].Text + lt2[1].Text);
                    word.Add(lt2[2].Text + lt2[3].Text + lt2[4].Text);
                    word.Add(lt2[5].Text + lt2[6].Text);
                    word.Add(lt2[7].Text + lt2[0].Text);
                    word.Add(lt2[1].Text + lt2[2].Text + lt2[6].Text);
                    word.Add(lt2[8].Text + lt2[4].Text);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // 새 문제 버튼
            grid_Answer.Children.Clear();
            question_board.Children.Clear();
            lt.Clear();
            lt2.Clear();
            Getitem2();
            plus_box2();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
            // 정답 맞추기 버튼
        {
            score();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            MessageBox.Show("소요시간 : " + ts.ToString("hh\\:mm\\:ss") + "\r\n" + count.ToString() + "개 정답", "채점 결과");
            stopwatch.Restart();
            count = 0;
            // 버튼을 누르면 count는 다시 0으로 초기화

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
            // 다시 풀기 버튼
        {
            grid_Answer.Children.Clear();
            question_board.Children.Clear();
            Getitem();
            lt.Clear();
            lt2.Clear();
            plus_textbox();
            

            // 다시 텍스트 박스 추가
        }


        private void seven_Selected(object sender, RoutedEventArgs e)
        {
            whole_grid.Children.Clear();
            whole_grid.RowDefinitions.Clear();
            whole_grid.ColumnDefinitions.Clear();
            sevenboard uc = new sevenboard();
            whole_grid.Children.Add(uc);
        }    
    }
}

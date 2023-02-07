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
    /// content.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class content : UserControl
    {
  

        public content(string id, string name, string phone, string area, string date)
        {
            InitializeComponent();
            tb_id.Text = id;
            tb_name.Text = name;
            tb_phone.Text = phone;
            tb_area.Text = area;
            tb_date.Text = DateTime.Now.ToString();
            // content라는 함수는 매개변수로 5개 값을 가짐
            // 이 값들은 textbox에 적혀있는 값들임

        }

        //private void create_button_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        using (Entities2 ei = new Entities2())
        //        {
        //            var create = ei.Set<TABLE2>();


        //            create.Add(new TABLE2
        //            {
        //                ID = tb_id.Text,
        //                NAME = tb_name.Text,
        //                PHONE = tb_phone.Text,
        //                AREA = tb_area.Text,
        //                DAY = DateTime.Now
        //                // textbox에 있는 내용들을 새로운 객체를 만들어서 생성
        //                // 날짜는 현재 날짜가 나오도록 설정
        //                // ID, NAME, PHONE, AREA, DAY는 TABLE2에 있는 값들
        //            });
        //            ei.SaveChanges();
        //            //db에서 쿼리문 실행과 동일
                    
        //        }
        //        MessageBox.Show("생성완료", "생성");
        //        // 성공을 하면 생성 완료됬다고 메시지가 나옴
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "ERROR");
        //        // 실패했을 경우 에러메시지가 나옴
        //    }
        //}

        //private void delete_button_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        using (Entities2 ei = new Entities2())
        //        {
        //            var del = ei.TABLE2.Where(d => d.ID == tb_id.Text).FirstOrDefault();
        //            // 첫번째 요소를 반환
        //            ei.TABLE2.Remove(del);
        //            // del에 의해 반환된 요소를 TABLE2에서 삭제
        //            ei.SaveChanges();
        //        }

        //        MessageBox.Show("삭제완료", "삭제");
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "ERROR");
        //    }
        //}
            

        //private void update_button_Click(object sender, RoutedEventArgs e)
        //{
        //    try{
        //        using (Entities2 ei = new Entities2())
        //        {
        //            var upd = ei.TABLE2.Where(u => u.ID == tb_id.Text).ToList();
        //            //table2에서 id가 tb_id의 이름과 같은 개체를 리스트형태로 가져옴

        //            upd[0].NAME = tb_name.Text;
        //            upd[0].PHONE = tb_phone.Text;
        //            upd[0].AREA = tb_area.Text;
        //            //가져온 개체의 이름, 전화번호, 지역을 textbox에 있는 내용으로 고침

        //            ei.SaveChanges();
        //        }
        //        MessageBox.Show("수정완료", "수정");
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "ERROR" );

        //    }
        //}

    }
}

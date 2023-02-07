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
    /// task7.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class task7 : UserControl
    {

      
        // Entities1 db;
        //List<item> items = null;
        public task7()
        {
            InitializeComponent();
            /*db = new Entities1();
            infolist.ItemsSource = db.TABLE2.ToList();*/
            infolist.ItemsSource = GetItems();
            pannel_selected.Children.Add(new content(null, null, null, null, null));
            // 처음에 버튼 보이게 하는 children.add

            

        }

        private List<item> GetItems()
        {
            List<item> items = new List<item>();
            using (Entities2 ei = new Entities2())
            {
                var query = from e in ei.TABLE2
                            select e;
                foreach (var a in query)
                {
                    items.Add(new item()
                    {                        
                        item_ID = a.ID,
                        item_NAME = a.NAME,
                        item_PHONE = a.PHONE,
                        item_AREA = a.AREA,
                        item_DAY = a.DAY.ToString()
                    });
                }
            }
            return items;
        }

        

        private void infolist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            item item = e.AddedItems[0] as item;
            pannel_selected.Children.Clear();
            pannel_selected.Children.Add(new content(item.item_ID, item.item_NAME, item.item_PHONE, item.item_AREA, item.item_DAY ));
        }

        


        private void create_button_Click(object sender, RoutedEventArgs e)
        {

            var cont = pannel_selected.Children[0] as content;
            // SelectionChanged에서 content 객체를 초기화 했음
            

            //foreach (var c in pannel_selected.Children)
            //{

            //}
           
            try
            {
                using (Entities2 ei = new Entities2())
                {
                    var create = ei.Set<TABLE2>();


                    create.Add(new TABLE2
                    {
                        ID = cont.tb_id.Text,
                        NAME = cont.tb_name.Text,
                        PHONE = cont.tb_phone.Text,
                        AREA = cont.tb_area.Text,
                        DAY = DateTime.Now
                        // textbox에 있는 내용들을 새로운 객체를 만들어서 생성
                        // 날짜는 현재 날짜가 나오도록 설정
                        // ID, NAME, PHONE, AREA, DAY는 TABLE2에 있는 값들
                    }) ;

                    ei.SaveChanges();
                    //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(infolist.ItemsSource);
                    //view.SortDescriptions.Add(new System.ComponentModel.SortDescription("item_ID", System.ComponentModel.ListSortDirection.Descending));
                    //db에서 쿼리문 실행과 동일

                }
                MessageBox.Show("생성완료", "생성");
                infolist.ItemsSource = GetItems();
                // 성공을 하면 생성 완료됬다고 메시지가 나옴
                // 메시지 박스를 띄운 후 바로 listview 아이템 갱신
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                // 실패했을 경우 에러메시지가 나옴
            }
        }

        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            var cont = pannel_selected.Children[0] as content;

            try
            {
                using (Entities2 ei = new Entities2())
                {
                    var upd = ei.TABLE2.Where(u => u.ID == cont.tb_id.Text).ToList();
                    //table2에서 id가 tb_id의 이름과 같은 개체를 리스트형태로 가져옴

                    upd[0].NAME = cont.tb_name.Text;
                    upd[0].PHONE = cont.tb_phone.Text;
                    upd[0].AREA = cont.tb_area.Text;
                    //가져온 개체의 이름, 전화번호, 지역을 textbox에 있는 내용으로 고침

                    ei.SaveChanges();
                }
                MessageBox.Show("수정완료", "수정");
                infolist.ItemsSource = GetItems();
                // 메시지 박스를 띄운 후 바로 listview 아이템 갱신
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");

            }

        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            var cont = pannel_selected.Children[0] as content;

            try
            {
                using (Entities2 ei = new Entities2())
                {
                    var del = ei.TABLE2.Where(d => d.ID == cont.tb_id.Text).FirstOrDefault();
                    // 첫번째 요소를 반환
                    ei.TABLE2.Remove(del);
                    // del에 의해 반환된 요소를 TABLE2에서 삭제
                    ei.SaveChanges();
                }

                MessageBox.Show("삭제완료", "삭제");
                infolist.ItemsSource = GetItems();
                // 메시지 박스를 띄운 후 바로 listview 아이템 갱신
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
    }
    public class item
    {
        public string item_ID { get; set; }
        public string item_NAME { get; set; }
        public string item_PHONE { get; set; }
        public string item_AREA { get; set; }
        public string item_DAY { get; set; }
    }
    
}

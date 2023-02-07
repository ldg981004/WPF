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
    /// bu_me2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class bu_me2 : UserControl
    {
        public bu_me2()
        {
            InitializeComponent();
            
            
        }

        private void Hyperlink_RequestNavigate(Object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            //절대 경로의 특정 사이트 열기

   
        }

        private void me2_1_MouseEnter(object sender, MouseEventArgs e)
        {
            image2_1.Source = new BitmapImage(new Uri("/Resources/me2-1-1.jpg", UriKind.RelativeOrAbsolute));
            image2_1.Visibility = Visibility.Visible;

            hy2_1.RequestNavigate +=  Hyperlink_RequestNavigate;
            hy2_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275368");
            
            
            hy1_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275368");

            tb1_1.Text = "서머너즈워-히트, 맞붙는 후속작";
            tb1_2.Text = null;
            tb1_3.Text = "8월 하반기 신작";

            image2_2.Source = new BitmapImage(new Uri("/Resources/me2-1-2.jpg", UriKind.RelativeOrAbsolute));
            image2_2.Visibility = Visibility.Visible;

            hy2_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275285");

            hy1_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275285");

            tb2_1.Text = "'무릎' 배재민, 철권 GOAT";
            tb2_2.Text = "의 다음 목표는";
            tb2_3.Text = "인터뷰";

            image2_3.Source = new BitmapImage(new Uri("/Resources/me2-1-3.jpg", UriKind.RelativeOrAbsolute));
            image2_3.Visibility = Visibility.Visible;

            hy2_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275217");

            hy1_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275217");

            tb3_1.Text = "오픈월드 신작 '타워 오";
            tb3_2.Text = "브 판타지',11일 정식..";
            tb3_3.Text = "게임뉴스";

            image2_4.Source = new BitmapImage(new Uri("/Resources/me2-1-4.jpg", UriKind.RelativeOrAbsolute));
            image2_4.Visibility = Visibility.Visible;

            hy2_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_4.NavigateUri = new Uri("https://minimap.net/magazine/newsletter-0817");

            hy1_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_4.NavigateUri = new Uri("https://minimap.net/magazine/newsletter-0817");

            tb4_1.Text = "길드 워2, 8월 23일 스팀";
            tb4_2.Text = "출시";
            tb4_3.Text = "뉴스레터";
        }

        private void me2_2_MouseEnter(object sender, MouseEventArgs e)
        {
            image2_1.Source = new BitmapImage(new Uri("/Resources/me2-2-1.jpg", UriKind.RelativeOrAbsolute));
            image2_1.Visibility = Visibility.Visible;

            hy2_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275526");

            hy1_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275526");

            tb1_1.Text = "'히트2' PC 버전 사전 다운로드 오픈";
            tb1_2.Text = null;
            tb1_3.Text = "게임 뉴스";

            image2_2.Source = new BitmapImage(new Uri("/Resources/me2-2-2.jpg", UriKind.RelativeOrAbsolute));
            image2_2.Visibility = Visibility.Visible;

            hy2_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275431");

            hy1_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275431");

            tb2_1.Text = "'유저 투표로 바뀌는 히";
            tb2_2.Text = "트2의 차별화..";
            tb2_3.Text = "게임 뉴스";

            image2_3.Source = new BitmapImage(new Uri("/Resources/me2-2-3.jpg", UriKind.RelativeOrAbsolute));
            image2_3.Visibility = Visibility.Visible;

            hy2_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=274833");

            hy1_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=274833");

            tb3_1.Text = "히트2가 걷고자 하는";
            tb3_2.Text = "'다른 길'";
            tb3_3.Text = "인터뷰";

            image2_4.Source = new BitmapImage(new Uri("/Resources/me2-2-4.jpg", UriKind.RelativeOrAbsolute));
            image2_4.Visibility = Visibility.Visible;

            hy2_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=274665");

            hy1_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=274833");

            tb4_1.Text = "'히트2' 8월 25일 정식";
            tb4_2.Text = "출시";
            tb4_3.Text = "게임뉴스";

        }

        private void me2_3_MouseEnter(object sender, MouseEventArgs e)
        {
            image2_1.Source = new BitmapImage(new Uri("/Resources/me2-3-1.jpg", UriKind.RelativeOrAbsolute));
            image2_1.Visibility = Visibility.Visible;

            hy2_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275547");
            

            hy1_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275547");

            tb1_1.Text = "툼레이더 인수한 엠브레이서, 반지의 제왕까지 품어";
            tb1_2.Text = null;
            tb1_3.Text = "게임 뉴스";

            image2_2.Source = new BitmapImage(new Uri("/Resources/me2-3-2.jpg", UriKind.RelativeOrAbsolute));
            image2_2.Visibility = Visibility.Visible;

            hy2_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275519");

            hy1_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275519");

            tb2_1.Text = "한국콘텐츠진흥원,";
            tb2_2.Text = "'게임더하기 지원사업'..";
            tb2_3.Text = "한국콘텐츠진흥원";

            image2_3.Source = new BitmapImage(new Uri("/Resources/me2-3-3.jpg", UriKind.RelativeOrAbsolute));
            image2_3.Visibility = Visibility.Visible;

            hy2_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275451");

            hy1_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275451");

            tb3_1.Text = "유니티, MS와 클라우드";
            tb3_2.Text = "파트너십 체결 발표";
            tb3_3.Text = "유니티";

            image2_4.Source = new BitmapImage(new Uri("/Resources/me2-3-4.png", UriKind.RelativeOrAbsolute));
            image2_4.Visibility = Visibility.Visible;

            hy2_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275450");

            hy1_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275450");

            tb4_1.Text = "네오위즈, '에오스 레드'";
            tb4_2.Text = "인넬라 X 플랫폼 온보딩..";
            tb4_3.Text = "네오위즈";
        }


        private void me2_4_MouseEnter(object sender, MouseEventArgs e)
        {
            image2_1.Source = new BitmapImage(new Uri("/Resources/me2-4-1.jpg", UriKind.RelativeOrAbsolute));
            image2_1.Visibility = Visibility.Visible;

            hy2_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275120");

            hy1_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275120");

            tb1_1.Text = "'위믹스, 3년이면 스팀 넘어서는 블록체인 플랫폼 된다'";
            tb1_2.Text = null;
            tb1_3.Text = "게임 뉴스";

            image2_2.Source = new BitmapImage(new Uri("/Resources/me2-4-2.jpg", UriKind.RelativeOrAbsolute));
            image2_2.Visibility = Visibility.Visible;

            hy2_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275519");

            hy1_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275519");

            tb2_1.Text = "핸드 트래킹, 이제 스마";
            tb2_2.Text = "트폰으로도 된..";
            tb2_3.Text = "게임 뉴스";

            image2_3.Source = new BitmapImage(new Uri("/Resources/me2-4-3.png", UriKind.RelativeOrAbsolute));
            image2_3.Visibility = Visibility.Visible;

            hy2_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275451");

            hy1_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275451");

            tb3_1.Text = "지금 살까? 메타 퀘스";
            tb3_2.Text = "트2 8월부터 10..";
            tb3_3.Text = "게임 뉴스";

            image2_4.Source = new BitmapImage(new Uri("/Resources/me2-4-4.jpg", UriKind.RelativeOrAbsolute));
            image2_4.Visibility = Visibility.Visible;

            hy2_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275450");
            

            hy1_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275451");

            tb4_1.Text = "'PS VR2, 외부 카메라";
            tb4_2.Text = "에 방송 기능..";
            tb4_3.Text = "게임 뉴스";

        }

        private void me2_5_MouseEnter(object sender, MouseEventArgs e)
        {
            image2_1.Source = new BitmapImage(new Uri("/Resources/me2-5-1.jpg", UriKind.RelativeOrAbsolute));
            image2_1.Visibility = Visibility.Visible;

            hy2_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275612");

            hy1_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275612");

            tb1_1.Text = "'검은 신화: 오공' 최신'";
            tb1_2.Text = "정보 담은 인..";
            tb1_3.Text = "동영상";

            image2_2.Source = new BitmapImage(new Uri("/Resources/me2-5-2.jpg", UriKind.RelativeOrAbsolute));
            image2_2.Visibility = Visibility.Visible;

            hy2_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275606");

            hy1_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275606");

            tb2_1.Text = "데스 스트랜딩, 8월 23";
            tb2_2.Text = "일 게임 패스로";
            tb2_3.Text = "동영상";

            image2_3.Source = new BitmapImage(new Uri("/Resources/me2-5-3.png", UriKind.RelativeOrAbsolute));
            image2_3.Visibility = Visibility.Visible;

            hy2_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275588");

            hy1_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275606");

            tb3_1.Text = "코믹 호러 '더 챈트'";
            tb3_2.Text = "신규 트레일..";
            tb3_3.Text = "동영상";

            image2_4.Source = new BitmapImage(new Uri("/Resources/me2-5-4.jpg", UriKind.RelativeOrAbsolute));
            image2_4.Visibility = Visibility.Visible;

            hy2_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275368");

            hy1_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275368");

            tb4_1.Text = "서머너즈위-히트,";
            tb4_2.Text = "맞붙는 후속작";
            tb4_3.Text = "8월 하반기 신작";
        }

        private void me2_6_MouseEnter(object sender, MouseEventArgs e)
        {
            image2_1.Source = new BitmapImage(new Uri("/Resources/me2-6-1.jpg", UriKind.RelativeOrAbsolute));
            image2_1.Visibility = Visibility.Visible;

            hy2_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_1.NavigateUri = new Uri("https://www.inven.co.kr/board/webzine/2898/2185");

            hy1_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_1.NavigateUri = new Uri("https://www.inven.co.kr/board/webzine/2898/2185");

            tb1_1.Text = "카케구루이 - 쟈바미 유메코 外";
            tb1_2.Text = null;
            tb1_3.Text = "에이크라운-아자";

            image2_2.Source = new BitmapImage(new Uri("/Resources/me2-6-2.jpg", UriKind.RelativeOrAbsolute));
            image2_2.Visibility = Visibility.Visible;

            hy2_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_2.NavigateUri = new Uri("https://www.inven.co.kr/board/webzine/2898/2184");

            hy1_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_2.NavigateUri = new Uri("https://www.inven.co.kr/board/webzine/2898/2184");

            tb2_1.Text = "승리의 여신: 니케-앨";
            tb2_2.Text = "리스";
            tb2_3.Text = "VVCOS-자몽";

            image2_3.Source = new BitmapImage(new Uri("/Resources/me2-6-3.jpg", UriKind.RelativeOrAbsolute));
            image2_3.Visibility = Visibility.Visible;

            hy2_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_3.NavigateUri = new Uri("https://www.inven.co.kr/board/webzine/2898/2183");

            hy1_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_3.NavigateUri = new Uri("https://www.inven.co.kr/board/webzine/2898/2183");

            tb3_1.Text = "페이트/그랜드 오더-";
            tb3_2.Text = "슈텐도지";
            tb3_3.Text = "에이크라운-햇냥";

            image2_4.Source = new BitmapImage(new Uri("/Resources/me2-6-4.jpg", UriKind.RelativeOrAbsolute));
            image2_4.Visibility = Visibility.Visible;

            hy2_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_4.NavigateUri = new Uri("https://www.inven.co.kr/board/webzine/2898/2182");

            hy1_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_4.NavigateUri = new Uri("https://www.inven.co.kr/board/webzine/2898/2182");

            tb4_1.Text = "명일방주 - 샤이닝";
            tb4_2.Text = null;
            tb4_3.Text = "에이크라운 - 웰";
        }

        private void me2_7_MouseEnter(object sender, MouseEventArgs e)
        {
            image2_1.Source = new BitmapImage(new Uri("/Resources/me2-7-1.jpg", UriKind.RelativeOrAbsolute));
            image2_1.Visibility = Visibility.Visible;

            hy2_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275419");

            hy1_1.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_1.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275419");

            tb1_1.Text = "빛바랜 추억이 아니다!";
            tb1_2.Text = "무궁무진한 도트 그래픽의 변화";
            tb1_3.Text = "리뷰";

            image2_2.Source = new BitmapImage(new Uri("/Resources/me2-7-2.jpg", UriKind.RelativeOrAbsolute));
            image2_2.Visibility = Visibility.Visible;

            hy2_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275414&site=er");

            hy1_2.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_2.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275414&site=er");

            tb2_1.Text = "ER 마스터즈: 부산";
            tb2_2.Text = "결승전 풍경기";
            tb2_3.Text = "이터널 리턴";

            image2_3.Source = new BitmapImage(new Uri("/Resources/me2-7-3.jpg", UriKind.RelativeOrAbsolute));
            image2_3.Visibility = Visibility.Visible;

            hy2_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275413&site=er");

            hy1_3.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_3.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275413&site=er");

            tb3_1.Text = "ER 페스티벌";
            tb3_2.Text = "루미아 야시장 오픈!";
            tb3_3.Text = "이터널 리턴";

            image2_4.Source = new BitmapImage(new Uri("/Resources/me2-7-4.jpg", UriKind.RelativeOrAbsolute));
            image2_4.Visibility = Visibility.Visible;

            hy2_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy2_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275412&site=er");

            hy1_4.RequestNavigate += Hyperlink_RequestNavigate;
            hy1_4.NavigateUri = new Uri("https://www.inven.co.kr/webzine/news/?news=275412&site=er");

            tb4_1.Text = "ER 페스티벌";
            tb4_2.Text = "코스프레 모음집";
            tb4_3.Text = "이터널 리턴";
        }

    }
}

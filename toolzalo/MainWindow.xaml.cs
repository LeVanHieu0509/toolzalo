using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
using KAutoHelper;
using System.Drawing;



namespace toolzalo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region data

        Bitmap ADD_FRIEND_ZALO;
        Bitmap SEND_ADD_FRIEND_ZALO;
        Bitmap COPY_ZALO;
        Bitmap SEND_FRIEND;
        Bitmap ZALO_LOGO;
        Bitmap NoCantFriend5Times;
        Bitmap Back;
        Bitmap DontSentFriend;
        Bitmap ThuHoi;
        Bitmap friendFull31;
        Bitmap GuiLaiSau;

        string messenger = "Chào chị! E thấy mình có quan tâm đến phụ kiện làm nails? Shop e chuyên cung cấp nails số lượng lớn sỉ lẻ toàn quốc. C kết bạn với e nhé";
        #endregion
        //add hình từ thư mục qua bitmap
        void LoadData()
        {
            ZALO_LOGO = (Bitmap)Bitmap.FromFile("Data//zaloLogo.png");
            ADD_FRIEND_ZALO = (Bitmap)Bitmap.FromFile("Data//AddFreind.png");
            SEND_FRIEND = (Bitmap)Bitmap.FromFile("Data//GuiYeuCauKB1.png");
            COPY_ZALO = (Bitmap)Bitmap.FromFile("Data//Dan.png");
            NoCantFriend5Times = (Bitmap)Bitmap.FromFile("Data//NoCantFriend5Times.png");
            Back = (Bitmap)Bitmap.FromFile("Data//Back.png");
            DontSentFriend = (Bitmap)Bitmap.FromFile("Data//DontSentFriend.png");
            ThuHoi = (Bitmap)Bitmap.FromFile("Data//ThuHoi.png");
            friendFull31 = (Bitmap)Bitmap.FromFile("Data//friendFull31.png");
            GuiLaiSau = (Bitmap)Bitmap.FromFile("Data//GuiLaiSau.png");
        }
        public MainWindow()
        {
            LoadData();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Task t = new Task(() =>
            {
                isStop = false;
                auto();
            });
            t.Start();
        }
        bool isStop = false;

        public void GetScreenPhoto(string deviceID, Bitmap bitmap)
        {
            var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);
            var AddImagePoint = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, bitmap);

            if (AddImagePoint != null)
            {
                if (isStop)
                    return;
                //click ket ban
                KAutoHelper.ADBHelper.Tap(deviceID, AddImagePoint.Value.X, AddImagePoint.Value.Y);
                delay(1);
            }
        }

       


        public void back(string deviceID, Bitmap bitmap)
        {
            GetScreenPhoto(deviceID, bitmap);
        }
        public void tap(string deviceID, float x, float y)
        {
            KAutoHelper.ADBHelper.TapByPercent(deviceID, x, y);
        }
    
        public void auto()
        {
            //Lấy ra danh sách devides id để dùng
            List<string> devices = new List<string>();
            var listDevice = KAutoHelper.ADBHelper.GetDevices();

           
            if (listDevice != null && listDevice.Count > 0)
            {
                devices = listDevice;
            }

            // chạy từng devide để điều khiển theo kịch bản bên trong
            foreach(var deviceID in devices)
            {

                //Tạo ra một luồng xử lý riêng biệt để xử lý
                Task t = new Task(() =>
                {
                    // CLick vào zalo
                    if (isStop)
                        return;
                    GetScreenPhoto(deviceID, ZALO_LOGO);
                    //KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.6, 17.6);
                    delay(5);

                    //Click vào menu danh bạ
                    if (isStop)
                        return;
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 37.7, 94.9);
                    delay(1);

                    //click vào Danh bạ máy
                    if (isStop)
                        return;
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 25.6, 25.7);
                    delay(1);

                    //click chưa là bạn
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 41.6, 28.2);
                    if (isStop)
                        return;

                    for(int i=0; i < 100; i++)
                    {
                        //lăn chuột
                        KAutoHelper.ADBHelper.Swipe(deviceID, 500, 900, 500, 700,300);
                        delay(2);

                        //click add friend
                        var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);
                        var AddImagePoint = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, ADD_FRIEND_ZALO);
                
                        if (AddImagePoint != null)
                        {

                            //click btn ket ban
                            KAutoHelper.ADBHelper.Tap(deviceID, AddImagePoint.Value.X, AddImagePoint.Value.Y);
                            delay(1);

                            var screenNoCantFriendDanhBaMay = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);
                            //check box btn 
                            var ShowDontSentFriendDanhBaMay = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenNoCantFriendDanhBaMay, DontSentFriend);

                            //check btn gửi lại sau
                            var ShowGuiLaiSauFriendDanhBaMay = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenNoCantFriendDanhBaMay, GuiLaiSau);
                            
                            if (ShowGuiLaiSauFriendDanhBaMay != null)
                            {

                                while (ShowGuiLaiSauFriendDanhBaMay != null)
                                {
                                    //click vào gửi lại sau
                                    KAutoHelper.ADBHelper.Tap(deviceID, ShowGuiLaiSauFriendDanhBaMay.Value.X, ShowGuiLaiSauFriendDanhBaMay.Value.Y);
                                    delay(1);

                                    //scroll
                                    KAutoHelper.ADBHelper.Swipe(deviceID, 500, 900, 500, 500, 100);
                                    delay(3);
                                    //chụp hình
                                    var screenAfterShowDontFreind = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);

                                    AddImagePoint = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenAfterShowDontFreind, ADD_FRIEND_ZALO);

                                    //Nếu có nút kết bạn thì click
                                    if (AddImagePoint != null)
                                    {
                                        KAutoHelper.ADBHelper.Tap(deviceID, AddImagePoint.Value.X, AddImagePoint.Value.Y);
                                    }

                                    //Hanl gửi lại sau trong khi ấn nút kết bạn
                                    var screenNoCantFriendDanhBaMayInWhile = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);
                                    
                                    var ShowGuiLaiSauFriendDanhBaMayInWhile = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenNoCantFriendDanhBaMayInWhile, GuiLaiSau);

                                    //Nếu như không có thì thoát khỏi vòng while
                                    if(ShowGuiLaiSauFriendDanhBaMayInWhile == null)
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        //Hanle nếu như không có nút kết bạn
                        if(AddImagePoint == null)
                        {
                            while(AddImagePoint == null)
                            {                     
                                var isScreen = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);
                                var isAddFriend = KAutoHelper.ImageScanOpenCV.FindOutPoint(isScreen, ADD_FRIEND_ZALO);
                                if(isAddFriend == null)
                                {
                                    KAutoHelper.ADBHelper.Swipe(deviceID, 500, 900, 500, 600, 100);
                                    delay(2);
                                }
                                else
                                {
                                    //click ket ban
                                    KAutoHelper.ADBHelper.Tap(deviceID, isAddFriend.Value.X, isAddFriend.Value.Y);
                                    delay(2);
                                    var screenNoCantFriendDanhBaMayInWhilePart2 = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);

                                    var ShowGuiLaiSauFriendDanhBaMayInWhilePart2 = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenNoCantFriendDanhBaMayInWhilePart2, GuiLaiSau);

                                    //handle kho click nút kết bạn nhảy qua nút gửi lại sau
                                    if (ShowGuiLaiSauFriendDanhBaMayInWhilePart2 != null)
                                    {
                                        while (ShowGuiLaiSauFriendDanhBaMayInWhilePart2 != null)
                                        {
                                            //click vào gửi lại sau
                                            KAutoHelper.ADBHelper.Tap(deviceID, ShowGuiLaiSauFriendDanhBaMayInWhilePart2.Value.X, ShowGuiLaiSauFriendDanhBaMayInWhilePart2.Value.Y);
                                            delay(1);

                                            //scroll
                                            KAutoHelper.ADBHelper.Swipe(deviceID, 500, 900, 500, 500, 100);
                                            delay(2);
                                            var screenAfterShowDontFreind = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);
                                            AddImagePoint = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenAfterShowDontFreind, ADD_FRIEND_ZALO);

                                            KAutoHelper.ADBHelper.Tap(deviceID, AddImagePoint.Value.X, AddImagePoint.Value.Y);

                                            var screenNoCantFriendDanhBaMayInWhilePart3 = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);

                                            var ShowGuiLaiSauFriendDanhBaMayInWhile1 = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenNoCantFriendDanhBaMayInWhilePart3, GuiLaiSau);

                                            if (ShowGuiLaiSauFriendDanhBaMayInWhile1 == null)
                                            {
                                                break;
                                            }
                                        }
                                    }

                                    break;
                                }
                               
                            }                          
                        }

                        //delay(2);  
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 94.3, 23.0);

                        //copy text
                        if (isStop)
                            return;
                      
                        //Gõ dẫu " "
                        KAutoHelper.ADBHelper.Key(deviceID, KAutoHelper.ADBKeyEvent.KEYCODE_SPACE);

                        //Click để copy text
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 24.1, 24.8);
                        delay(1);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 4.2, 26.0);
                        delay(1);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 11.8, 18.6);
                        delay(1);

                        //add friend final
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.0, 45.3);
                        delay(2);
                        //check lỗi khi add friend 
                        var screenNoCantFriendError = KAutoHelper.ADBHelper.ScreenShoot(deviceID, false);

                        var ShowNoCantFriend5Times = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenNoCantFriendError, NoCantFriend5Times);
                        var ShowDontSentFriend = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenNoCantFriendError, DontSentFriend);
                        var ShowFullSentFriend = KAutoHelper.ImageScanOpenCV.FindOutPoint(screenNoCantFriendError, friendFull31);

                        //Nếu lỗi kết bạn quá 5 người (lỗi 32)
                        if (ShowNoCantFriend5Times != null)
                        {
                            back(deviceID, Back);
                        }
                        //Nếu lỗi kết bạn không thể gửi (Lỗi 40)
                        if (ShowDontSentFriend != null)
                        {
                            //click ra màn hình
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 60.2, 6.5);
                            back(deviceID, Back);
                        }
                        //Nếu lỗi kết bạn quá 5 người (Lỗi 31)
                        if (ShowFullSentFriend != null)
                        {
                            back(deviceID, Back);
                        }
                        //tìm lỗi không thể gửi
                    }
                });

                t.Start();
            }
        }
        public void delay(int delay)
        {
            while(delay > 0){
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delay--;
                if (isStop)
                    break;
            }
        }
    }
}

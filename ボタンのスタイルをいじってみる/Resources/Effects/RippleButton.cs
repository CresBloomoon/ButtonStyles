using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ボタンのスタイルをいじってみる.Resources.Effects {
    internal sealed class RippleButton : Button{
        public Brush RippleColor {
            get => (Brush)GetValue(RippleColorProperty);
            set => SetValue(RippleColorProperty, value);
        }

        public static readonly DependencyProperty RippleColorProperty =
            DependencyProperty.Register("RippleColor", typeof(Brush),
                typeof(RippleButton), new PropertyMetadata(Brushes.White));

        /// <summary>
        /// コントロールのテンプレートが適用されたときの呼ばれる。
        /// テンプレートが適用されると、ボタンにマウスダウンイベントのハンドラを追加する
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            this.AddHandler(MouseDownEvent, new RoutedEventHandler(this.OnMouseDown), true);
        }

        /// <summary>
        /// マウスがボタン上でホバーされているときにクリックされると呼ばれるメソッド
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベントデータ</param>
        public void OnMouseDown(object sender, RoutedEventArgs e) {

            //マウスポインタの位置を取得する
            //座標は、コントロール内の相対座標で表される。
            Point mousePosition = (e as MouseButtonEventArgs).GetPosition(this);

            //テンプレート内のCircleEffectという名前の要素（通常は円形の波紋効果）を取得
            //取得した要素はEllipseオブジェクトにキャストする
            var temp = GetTemplateChild("CircleEffect");
            var ellipse = GetTemplateChild("CircleEffect") as Ellipse;

            //取得した波紋効果の位置を指定する。これにより波紋効果がマウスの位置に表示されるようになる。
            ellipse.Margin = new Thickness(mousePosition.X, mousePosition.Y, 0, 0);

            //xamlリソースから、RippleAnimationという名前のアニメーションを取得し、そのクローンを生成する。
            //取得したアニメーションは、波紋効果を表示するためにのちに使用する。
            Storyboard storyboard = (FindResource("RippleAnimation") as Storyboard).Clone();

            //波紋効果の最大サイズ
            //ボタンの幅と高さの最大値の3倍を設定した。
            double effectMaxSize = Math.Max(ActualWidth, ActualHeight) * 3;

            //波紋効果の初期位置を設定
            (storyboard.Children[2] as ThicknessAnimation).From = new Thickness(mousePosition.X, mousePosition.Y, 0, 0);

            //波紋効果の最終位置を設定
            (storyboard.Children[2] as ThicknessAnimation).To = new Thickness(mousePosition.X - effectMaxSize / 2, mousePosition.Y - effectMaxSize / 2, 0, 0);
            
            //波紋効果のサイズを設定
            (storyboard.Children[3] as DoubleAnimation).To = effectMaxSize;

            //アニメーション開始
            ellipse.BeginStoryboard(storyboard);
        }
    }
}

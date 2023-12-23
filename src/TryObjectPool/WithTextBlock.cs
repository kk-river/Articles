using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Extensions.ObjectPool;

namespace TryObjectPool;

internal class WithTextBlock : Canvas
{
    private readonly ObjectPoolProvider _objectPoolProvider;
    private readonly ObjectPool<TextBlock> _objectPool;

    public WithTextBlock()
    {
        _objectPoolProvider = new DefaultObjectPoolProvider();
        _objectPool = _objectPoolProvider.Create(new TextBlockPooledObjectPolicy(this));
    }

    public void OnRowShown(ContentControl row)
    {
        TextBlock textBlock = _objectPool.Get();
        textBlock.Text = "Hello, World!"; //テキストだけ設定すればOK

        row.Content = textBlock;
    }

    public void OnRowHidden(ContentControl row)
    {
        _objectPool.Return((TextBlock)row.Content);
    }

    public class TextBlockPooledObjectPolicy : PooledObjectPolicy<TextBlock>
    {
        private readonly FrameworkElement _parent; //適当な親要素が渡されているとして、そこからリソースを取得する

        public TextBlockPooledObjectPolicy(FrameworkElement parent)
        {
            _parent = parent;
        }

        public override TextBlock Create() => new()
        {
            Background = _parent.TryFindResource("MyBackground") as Brush ?? Brushes.AliceBlue, //リソースから背景を設定し、
            Foreground = _parent.TryFindResource("MyForeground") as Brush ?? Brushes.Black, //リソースから文字色を設定し、
            VerticalAlignment = VerticalAlignment.Center, //縦方向の配置を設定し、
            HorizontalAlignment = HorizontalAlignment.Center, //横方向の配置を設定し、
            FontSize = 24, //フォントサイズを設定し、
            FontFamily = new FontFamily("Yu Gothic UI"), //フォントを設定し、
            FontWeight = FontWeights.Bold, //フォントの太さを設定し、
            TextWrapping = TextWrapping.Wrap, //テキストの折り返しを設定し、
            TextTrimming = TextTrimming.CharacterEllipsis, //テキストの末尾を省略記号で省略するように設定し、
            TextAlignment = TextAlignment.Center, //テキストの配置を設定する
        };

        public override bool Return(TextBlock obj)
        {
            obj.Text = string.Empty; //毎回設定するのであれば、ここで消さなくても良い
            return true;
        }
    }
}

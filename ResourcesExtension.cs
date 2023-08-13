namespace liúdáchéng.WPF.Markup;



/// <summary>
/// <para> 寻找资源 </para>
/// <para>  - 从控件中查找资源 </para>
/// <para>  - 从应用程序查找资源 </para>
/// <para>  - 没找到返回 default </para>
/// </summary>
/// <returns>
/// 返回的资源。或者 default。
/// </returns>

[MarkupExtensionReturnType( typeof( object ) )]
[Localizability( LocalizationCategory.NeverLocalize )]
public class ResourcesExtension : MarkupExtension, IMarkupService
{
    [ConstructorArgument( nameof( ResourceKey ) )]
    public object ResourceKey { get; set; }

    public ResourcesExtension( object key )
    {
        this.ResourceKey = key ?? string.Empty;
    }

    public override object ProvideValue( IServiceProvider serviceProvider )
    {
        var target = IMarkupService.GetProvideValueTarget( serviceProvider );



        if ( this.ResourceKey != null && !object.ReferenceEquals( this.ResourceKey, string.Empty ) )
        {
            var ui = target.TargetObject as FrameworkElement;
            var css = ui?.Resources;

            if ( css != null && css.Contains( this.ResourceKey ) )
            {
                return css[ this.ResourceKey ];
            }

            css = Application.Current.Resources;
            if ( css.Contains( this.ResourceKey ) )
            {
                return css[ this.ResourceKey ];
            }
        }


        var dependency = target.TargetProperty as Property;
        if ( dependency.PropertyType.IsAssignableTo( typeof( ValueType ) ) )
        {
            return dependency.PropertyType.Create();
        }

        return null;
    }
}

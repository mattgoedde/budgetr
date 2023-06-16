using Budgetr.Ui.Components.Layout;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace Budgetr.Components.Layout;

public class Div : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Class { get; set; }

    protected virtual string ClassBuilder()
    {
        var sb = new StringBuilder();

        if (Margin is not null) sb.Append($" {margin}");
        if (MarginX is not null) sb.Append($" {marginX}");
        if (MarginY is not null) sb.Append($" {marginY}");

        if (Padding is not null) sb.Append($" {padding}");
        if (PaddingX is not null) sb.Append($" {paddingX}");
        if (PaddingY is not null) sb.Append($" {paddingY}");

        if (Class is not null) sb.Append($" {Class}");

        return sb.ToString();
    }

    [Parameter]
    public string? Style { get; set; }
    protected virtual string StyleBuilder() => Style is not null ? $"{Style}" : "";

    [Parameter]
    public Spacing.Margin? Margin { get; set; }
    string? margin => Margin switch
    {
        Spacing.Margin.ExtraSmall => "m-xs",
        Spacing.Margin.Small => "m-s",
        Spacing.Margin.Medium => "m-m",
        Spacing.Margin.Large => "m-l",
        Spacing.Margin.ExtraLarge => "m-xl",
        _ => null
    };

    [Parameter]
    public Spacing.Margin? MarginX { get; set; }
    string? marginX => MarginX switch
    {
        Spacing.Margin.ExtraSmall => "m-x-xs",
        Spacing.Margin.Small => "m-x-s",
        Spacing.Margin.Medium => "m-x-m",
        Spacing.Margin.Large => "m-x-l",
        Spacing.Margin.ExtraLarge => "m-x-xl",
        _ => null
    };

    [Parameter]
    public Spacing.Margin? MarginY { get; set; }
    string? marginY => MarginY switch
    {
        Spacing.Margin.ExtraSmall => "m-y-xs",
        Spacing.Margin.Small => "m-y-s",
        Spacing.Margin.Medium => "m-y-m",
        Spacing.Margin.Large => "m-y-l",
        Spacing.Margin.ExtraLarge => "m-y-xl",
        _ => null
    };

    [Parameter]
    public Spacing.Padding? Padding { get; set; }
    string? padding => Padding switch
    {
        Spacing.Padding.ExtraSmall => "p-xs",
        Spacing.Padding.Small => "p-s",
        Spacing.Padding.Medium=> "p-m",
        Spacing.Padding.Large => "p-l",
        Spacing.Padding.ExtraLarge => "p-xl",
        _ => null
    };

    [Parameter]
    public Spacing.Padding? PaddingX { get; set; }
    string? paddingX => PaddingX switch
    {
        Spacing.Padding.ExtraSmall => "p-x-xs",
        Spacing.Padding.Small => "p-x-s",
        Spacing.Padding.Medium => "p-x-m",
        Spacing.Padding.Large => "p-x-l",
        Spacing.Padding.ExtraLarge => "p-x-xl",
        _ => null
    };

    [Parameter]
    public Spacing.Padding? PaddingY { get; set; }
    string? paddingY => PaddingY switch
    {
        Spacing.Padding.ExtraSmall => "p-y-xs",
        Spacing.Padding.Small => "p-y-s",
        Spacing.Padding.Medium => "p-y-m",
        Spacing.Padding.Large => "p-y-l",
        Spacing.Padding.ExtraLarge => "p-y-xl",
        _ => null
    };
}
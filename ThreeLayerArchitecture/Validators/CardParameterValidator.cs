using FluentValidation;
using ThreeLayerArchitecture.Model;

namespace ThreeLayerArchitecture.Validators;

/// <summary>
/// Card Parameter 驗證器
/// </summary>
public class CardParameterValidator : AbstractValidator<CardParameter>
{
    public CardParameterValidator()
    {
        this.RuleFor(card => card.Attack)
            .GreaterThanOrEqualTo(0);

        // 串列
        this.RuleForEach(card => card.Name)
            .NotEmpty();

        this.RuleFor(card => card.Attack)
            .Must(attack => attack >= 0 && attack <= 3000);
    }
}

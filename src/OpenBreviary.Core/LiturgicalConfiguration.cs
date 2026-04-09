namespace OpenBreviary.Core
{
    public record LiturgicalConfiguration(
        bool EpiphanyTransferredToSunday = true,
        bool AscensionTransferredToSunday = false
    );
}

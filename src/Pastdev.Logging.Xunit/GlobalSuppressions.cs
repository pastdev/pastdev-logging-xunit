using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "StyleCop.CSharp.MaintainabilityRules",
    "SA1402:FileMayOnlyContainASingleType",
    Justification = "How else would we name generic and non generic (https://stackoverflow.com/a/4063061/516433).",
    Scope = "type",
    Target = "~T:Pastdev.Logging.Xunit.XunitLoggerProvider`1<!!0>")]

[assembly: SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:ElementsShouldBeDocumented",
    Justification = "Test class/method naming convention serves as documentation.")]

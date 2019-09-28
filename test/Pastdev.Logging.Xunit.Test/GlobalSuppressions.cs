using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Microsoft.Design",
    "CA1034:NestedTypesShouldNotBeVisible",
    Justification = "Test class/method naming convention uses nested classes to represent method under test.")]

[assembly: SuppressMessage(
    "Microsoft.Design",
    "CA1303:DoNotPassLiteralsAsLocalizedParameters",
    Justification = "No intention to ever localize.")]

[assembly: SuppressMessage(
    "StyleCop.CSharp.OrderingRules",
    "SA1202:ElementsMustBeOrderedByAccess",
    Justification = "Alphabetization works better for organization.")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.OrderingRules",
    "SA1204:StaticElementsMustAppearBeforeInstanceElements",
    Justification = "Alphabetization works better for organization.")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.OrderingRules",
    "SA1214:ReadonlyFieldsShouldAppearBefore",
    Justification = "Alphabetization works better for organization.")]

[assembly: SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:ElementsShouldBeDocumented",
    Justification = "Test class/method naming convention serves as documentation.")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1633:FileMustHaveHeader",
    Justification = "Dont have a header for this project code.")]
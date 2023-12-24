using Application.Common.Constants;
using FluentValidation;

namespace Application.EntityManagement.Addresses.Dtos.AddressDto;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    private readonly string[] _countries =
    {
        "AFGHANISTAN",
        "ALBANIA",
        "ALGERIA",
        "ANDORRA",
        "ANGOLA",
        "ANTIGUA AND BARBUDA",
        "ARGENTINA",
        "ARMENIA",
        "AUSTRALIA",
        "AUSTRIA",
        "AZERBAIJAN",
        "BAHAMAS",
        "BAHRAIN",
        "BANGLADESH",
        "BARBADOS",
        "BELARUS",
        "BELGIUM",
        "BELIZE",
        "BENIN",
        "BHUTAN",
        "BOLIVIA",
        "BOSNIA AND HERZEGOVINA",
        "BOTSWANA",
        "BRAZIL",
        "BRUNEI",
        "BULGARIA",
        "BURKINA FASO",
        "BURUNDI",
        "CABO VERDE",
        "CAMBODIA",
        "CAMEROON",
        "CANADA",
        "CENTRAL AFRICAN REPUBLIC",
        "CHAD",
        "CHILE",
        "CHINA",
        "COLOMBIA",
        "COMOROS",
        "CONGO",
        "COSTA RICA",
        "CROATIA",
        "CUBA",
        "CYPRUS",
        "CZECH REPUBLIC",
        "DENMARK",
        "DJIBOUTI",
        "DOMINICA",
        "DOMINICAN REPUBLIC",
        "EAST TIMOR",
        "ECUADOR",
        "EGYPT",
        "EL SALVADOR",
        "ENGLAND",
        "EQUATORIAL GUINEA",
        "ERITREA",
        "ESTONIA",
        "ESWATINI",
        "ETHIOPIA",
        "FIJI",
        "FINLAND",
        "FRANCE",
        "GABON",
        "GAMBIA",
        "GEORGIA",
        "GERMANY",
        "GHANA",
        "GREECE",
        "GRENADA",
        "GUATEMALA",
        "GUINEA",
        "GUINEA-BISSAU",
        "GUYANA",
        "HAITI",
        "HONDURAS",
        "HUNGARY",
        "ICELAND",
        "INDIA",
        "INDONESIA",
        "IRAN",
        "IRAQ",
        "IRELAND",
        "ISRAEL",
        "ITALY",
        "IVORY COAST",
        "JAMAICA",
        "JAPAN",
        "JORDAN",
        "KAZAKHSTAN",
        "KENYA",
        "KIRIBATI",
        "KOREA, NORTH",
        "KOREA, SOUTH",
        "KOSOVO",
        "KUWAIT",
        "KYRGYZSTAN",
        "LAOS",
        "LATVIA",
        "LEBANON",
        "LESOTHO",
        "LIBERIA",
        "LIBYA",
        "LIECHTENSTEIN",
        "LITHUANIA",
        "LUXEMBOURG",
        "MADAGASCAR",
        "MALAWI",
        "MALAYSIA",
        "MALDIVES",
        "MALI",
        "MALTA",
        "MARSHALL ISLANDS",
        "MAURITANIA",
        "MAURITIUS",
        "MEXICO",
        "MICRONESIA",
        "MOLDOVA",
        "MONACO",
        "MONGOLIA",
        "MONTENEGRO",
        "MOROCCO",
        "MOZAMBIQUE",
        "MYANMAR",
        "NAMIBIA",
        "NAURU",
        "NEPAL",
        "NETHERLANDS",
        "NEW ZEALAND",
        "NICARAGUA",
        "NIGER",
        "NIGERIA",
        "NORTH MACEDONIA",
        "NORWAY",
        "OMAN",
        "PAKISTAN",
        "PALAU",
        "PANAMA",
        "PAPUA NEW GUINEA",
        "PARAGUAY",
        "PERU",
        "PHILIPPINES",
        "POLAND",
        "PORTUGAL",
        "QATAR",
        "ROMANIA",
        "RUSSIA",
        "RWANDA",
        "SAINT KITTS AND NEVIS",
        "SAINT LUCIA",
        "SAINT VINCENT AND THE GRENADINES",
        "SAMOA",
        "SAN MARINO",
        "SAO TOME AND PRINCIPE",
        "SAUDI ARABIA",
        "SENEGAL",
        "SERBIA",
        "SEYCHELLES",
        "SIERRA LEONE",
        "SINGAPORE",
        "SLOVAKIA",
        "SLOVENIA",
        "SOLOMON ISLANDS",
        "SOMALIA",
        "SOUTH AFRICA",
        "SOUTH SUDAN",
        "SPAIN",
        "SRI LANKA",
        "SUDAN",
        "SURINAME",
        "SWEDEN",
        "SWITZERLAND",
        "SYRIA",
        "TAIWAN",
        "TAJIKISTAN",
        "TANZANIA",
        "THAILAND",
        "TOGO",
        "TONGA",
        "TRINIDAD AND TOBAGO",
        "TUNISIA",
        "TURKEY",
        "TURKMENISTAN",
        "TUVALU",
        "UGANDA",
        "UKRAINE",
        "UNITED ARAB EMIRATES",
        "UNITED KINGDOM",
        "UNITED STATES",
        "URUGUAY",
        "UZBEKISTAN",
        "VANUATU",
        "VATICAN CITY",
        "VENEZUELA",
        "VIETNAM",
        "YEMEN",
        "ZAMBIA",
        "ZIMBABWE"
    };

    public AddressDtoValidator()
    {
        RuleFor(addressDto => addressDto.PostalCode)
            .NotEmpty()
            .WithMessage("PostalCode is required.")
            .MinimumLength(5)
            .WithMessage("PostalCode cannot be less than 5 characters.")
            .MaximumLength(10)
            .WithMessage("PostalCode cannot exceed 10 characters.")
            .Matches(RegexPatternConstants.PostalCodePattern)
            .WithMessage("Invalid PostalCode format.");

        RuleFor(addressDto => addressDto.UnitNumber)
            .MaximumLength(20)
            .WithMessage("UnitNumber cannot exceed 20 characters.")
            .Matches(RegexPatternConstants.UnitNumberPattern)
            .WithMessage("Invalid unit number format.");

        RuleFor(addressDto => addressDto.BuildingNumber)
            .MaximumLength(20)
            .WithMessage("BuildingNumber cannot exceed 20 characters.");

        RuleFor(addressDto => addressDto.Street)
            .MaximumLength(100)
            .WithMessage("Street cannot exceed 100 characters.");

        RuleFor(addressDto => addressDto.City)
            .NotEmpty()
            .WithMessage("City is required.")
            .MaximumLength(50)
            .WithMessage("City cannot exceed 50 characters.");

        RuleFor(addressDto => addressDto.State)
            .MaximumLength(50)
            .WithMessage("State cannot exceed 50 characters.");

        RuleFor(addressDto => addressDto.Country)
            .NotEmpty()
            .WithMessage("Country is required.")
            .Must(country => _countries.Contains(country.ToUpperInvariant()))
            .WithMessage("Invalid country name.");
    }
}
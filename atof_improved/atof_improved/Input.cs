using CsvHelper.Configuration.Attributes;
namespace atof_improved
{
    public class Input
    {
        [Name("Datum")]
        public string Date { get; set; }

        [Name("Rezultat")]
        public string Result { get; set; }

        [Name("Komentar")]
        public string Comment { get; set; }

    }
}

namespace atof_improved
{
    public class OutputData
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int NumberOfMeasures { get; set; }
        public double Sum { get; set; }

        public string getNameOfMonth(int num)
        {
            switch (num)
            {
                case 1:
                    return "Januar";
                case 2:
                    return "Februar";
                case 3:
                    return "Mart";
                case 4:
                    return "April";
                case 5:
                    return "Maj";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Avgust";
                case 9:
                    return "Septembar";
                case 10:
                    return "Oktobar";
                case 11:
                    return "Novembar";
                case 12:
                    return "Decembar";
                default:
                    return "Greska";
            }
        }
    }
}

namespace bookFlow.Enum
{
    public enum LoanStatus
    {

        EN_COURS = 0,   // Loan is currently active
        RETOURNE = 1,   // Book has been returned
        EN_RETARD = 2,  // Loan is overdue
        ANNULE = 3      // Loan was cancelled by the user
    }
}

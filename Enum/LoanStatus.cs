namespace bookFlow.Enum
{
    public enum LoanStatus
    {
        EN_COURS = 0,   // Loan created but not approved yet (user can cancel)
        APPROVED = 1,   // Loan approved by admin (user cannot cancel)
        RETOURNE = 2,   // Book has been returned
        EN_RETARD = 3,  // Loan is overdue
        ANNULE = 4      // Loan was cancelled by the user
    }
}

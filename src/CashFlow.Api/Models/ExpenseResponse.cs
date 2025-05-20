namespace CashFlow.Api.Models;

/// <summary>
/// Response model for expense operations
/// </summary>
public class ExpenseResponse
{
    /// <summary>
    /// Unique identifier for the expense
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Description of the expense
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Amount spent
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Date and time of the expense
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// When the expense was created in the system
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

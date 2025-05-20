using System.ComponentModel.DataAnnotations;

namespace CashFlow.Api.Models;

/// <summary>
/// Data transfer object for expense operations
/// </summary>
public class ExpenseDto
{
    /// <summary>
    /// Description of the expense
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Amount spent
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Date and time of the expense
    /// </summary>
    [Required]
    public DateTime Date { get; set; }
}

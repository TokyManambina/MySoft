using System.ComponentModel.DataAnnotations;

namespace SoftSignAPI.Model
{
	public class Historique
	{
		[Key]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public ActionList Action { get; set; }
		public string? Table { get; set; }
		public string? Colonne { get; set; }
		public string? OldValue { get; set; }
		public string? NewValue { get; set; }
		public string? TableId { get; set; }
	}

	public enum ActionList
	{
		Insert, Update, Delete
	}
}

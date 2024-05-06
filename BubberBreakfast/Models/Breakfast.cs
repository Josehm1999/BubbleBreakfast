using Postgrest.Attributes;
using Postgrest.Models;

namespace BubberBreakfast.Models;

[Table("Breakfast")]
public class Breakfast : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    [Column("Description")]
    public string Description { get; set; }

    [Column("StartDateTime")]
    public DateTime StartDateTime { get; set; }

    [Column("EndDateTime")]
    public DateTime EndDateTime { get; set; }

    [Column("LastTimeModified")]
    public DateTime LastTimeModified { get; set; }

    [Column("Savory")]
    public List<string> Savory { get; set; }

    [Column("Sweet")]
    public List<string> Sweet { get; set; }

}

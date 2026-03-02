namespace Blackout.Models.Data;

[Table("Settings", Schema = "cfg")]
public class Setting
{
    [Key]
    public int SettingID { get; set; }

    public string Mail { get; set; } = string.Empty;

    public bool sendingMailEnabled { get; set; }




}
namespace Dungeon.Networking
{
	public class PlayerNetworking
	{
		public string name { get; set; }
		public int maxLvl { get; set; }
		public int maxXP { get; set; }
		public int mobsKilled { get; set; }

		public PlayerNetworking(string name, int maxLvl, int maxXP, int mobsKilled)
		{
			this.name = name;
			this.maxLvl = maxLvl;
			this.maxXP = maxXP;
			this.mobsKilled = mobsKilled;
		}
	}

}

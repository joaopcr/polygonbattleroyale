public class ArmaItem
{
	private Arma armaModel;
	private int ammo;

	public ArmaItem (){}

	public ArmaItem (Arma am, int a)
	{
		this.armaModel = am;
		this.ammo = a;
	}

	public Arma getArmaModel ()
	{
		return this.armaModel;
	}

	public int getAmmo ()
	{
		return this.ammo;
	}

	public void setArmaModel (Arma arma)
	{
		this.armaModel = arma;
	}

	public void setAmmo (int ammo)
	{
		this.ammo = ammo;
	}
}
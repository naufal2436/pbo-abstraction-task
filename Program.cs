interface Kemampuan
{
    void gunakan(Robot robot);
    boolean cooldownSelesai();
}

abstract class Robot
{
    protected String nama;
    protected int energi;
    protected int armor;
    protected int serangan;

    public Robot(String nama, int energi, int armor, int serangan)
    {
        this.nama = nama;
        this.energi = energi;
        this.armor = armor;
        this.serangan = serangan;
    }

    public void serang(Robot target)
    {
        int damage = serangan - target.armor;
        if (damage > 0)
        {
            target.energi -= damage;
            System.out.println(nama + " menyerang " + target.nama + " dengan damage " + damage + "!");
        }
        else
        {
            System.out.println(nama + " menyerang " + target.nama + " tetapi tidak ada damage!");
        }
    }

    public void cetakInformasi()
    {
        System.out.println("Nama: " + nama);
        System.out.println("Energi: " + energi);
        System.out.println("Armor: " + armor);
        System.out.println("Serangan: " + serangan);
    }

    public abstract void gunakanKemampuan(Kemampuan kemampuan);

    public boolean masihHidup()
    {
        return energi > 0;
    }
}

// Class Robot biasa
class RobotBiasa extends Robot
{

    public RobotBiasa(String nama, int energi, int armor, int serangan)
{
    super(nama, energi, armor, serangan);
}

@Override
    public void gunakanKemampuan(Kemampuan kemampuan)
{
    if (kemampuan.cooldownSelesai())
    {
        kemampuan.gunakan(this);
    }
    else
    {
        System.out.println("Kemampuan masih dalam cooldown!");
    }
}
}

// Class BosRobot yang mewarisi dari Robot
class BosRobot extends Robot
{
    private int pertahanan;

public BosRobot(String nama, int energi, int armor, int serangan, int pertahanan)
{
    super(nama, energi, armor, serangan);
    this.pertahanan = pertahanan;
}

@Override
    public void gunakanKemampuan(Kemampuan kemampuan)
{
    if (kemampuan.cooldownSelesai())
    {
        kemampuan.gunakan(this);
    }
    else
    {
        System.out.println("Kemampuan masih dalam cooldown!");
    }
}

public void diserang(Robot penyerang)
{
    int damage = penyerang.serangan - (armor + pertahanan);
    if (damage > 0)
    {
        energi -= damage;
        System.out.println(nama + " diserang oleh " + penyerang.nama + " dengan damage " + damage + "!");
    }
    else
    {
        System.out.println(nama + " berhasil menahan serangan dari " + penyerang.nama + "!");
    }

    if (energi <= 0)
    {
        mati();
    }
}

public void mati()
{
    System.out.println(nama + " telah mati!");
}
}

// Implementasi Kemampuan Repair (memulihkan energi)
class Perbaikan implements Kemampuan
{
    private int pemulihan;
private int cooldown;
private int cooldownTimer;

public Perbaikan(int pemulihan, int cooldown)
{
    this.pemulihan = pemulihan;
    this.cooldown = cooldown;
    this.cooldownTimer = 0;
}

@Override
    public void gunakan(Robot robot)
{
    robot.energi += pemulihan;
    System.out.println(robot.nama + " menggunakan kemampuan Perbaikan dan memulihkan " + pemulihan + " energi!");
    cooldownTimer = cooldown;
}

@Override
    public boolean cooldownSelesai()
{
    return cooldownTimer == 0;
}

public void kurangiCooldown()
{
    if (cooldownTimer > 0)
    {
        cooldownTimer--;
    }
}
}

// Implementasi Kemampuan Electric Shock (serangan listrik)
class SeranganListrik implements Kemampuan
{
    private int damage;
private int cooldown;
private int cooldownTimer;

public SeranganListrik(int damage, int cooldown)
{
    this.damage = damage;
    this.cooldown = cooldown;
    this.cooldownTimer = 0;
}

@Override
    public void gunakan(Robot robot)
{
    System.out.println(robot.nama + " menggunakan kemampuan Serangan Listrik dan memberikan damage " + damage + "!");
    cooldownTimer = cooldown;
}

@Override
    public boolean cooldownSelesai()
{
    return cooldownTimer == 0;
}

public void kurangiCooldown()
{
    if (cooldownTimer > 0)
    {
        cooldownTimer--;
    }
}
}

// Implementasi Kemampuan Plasma Cannon (serangan plasma)
class SeranganPlasma implements Kemampuan
{
    private int damage;
private int cooldown;
private int cooldownTimer;

public SeranganPlasma(int damage, int cooldown)
{
    this.damage = damage;
    this.cooldown = cooldown;
    this.cooldownTimer = 0;
}

@Override
    public void gunakan(Robot robot)
{
    System.out.println(robot.nama + " menggunakan kemampuan Serangan Plasma dan memberikan damage " + damage + "!");
    cooldownTimer = cooldown;
}

@Override
    public boolean cooldownSelesai()
{
    return cooldownTimer == 0;
}

public void kurangiCooldown()
{
    if (cooldownTimer > 0)
    {
        cooldownTimer--;
    }
}
}

// Main class untuk menjalankan permainan
public class SimulatorPertarunganRobot
{
    public static void main(String[] args)
    {
        // Membuat robot biasa
        RobotBiasa robot1 = new RobotBiasa("Robot A", 100, 10, 20);
        RobotBiasa robot2 = new RobotBiasa("Robot B", 80, 15, 15);

        // Membuat bos robot
        BosRobot bosRobot = new BosRobot("Bos X", 200, 20, 30, 10);

        // Membuat kemampuan
        Kemampuan perbaikan = new Perbaikan(30, 3);
        Kemampuan listrik = new SeranganListrik(25, 2);
        Kemampuan plasma = new SeranganPlasma(40, 4);

        // Pertarungan
        robot1.cetakInformasi();
        robot2.cetakInformasi();
        bosRobot.cetakInformasi();

        robot1.serang(bosRobot);
        bosRobot.diserang(robot1);

        robot2.gunakanKemampuan(plasma);
        robot2.gunakanKemampuan(listrik);
    }
}

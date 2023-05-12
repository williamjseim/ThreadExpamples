namespace lone
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
public interface ITeleport
{
    void Teleport(int x, int y);
}

public interface IHeal
{
    void Heal();
}

public interface IDie
{
    void Die();
}

public interface IThrowMagicMissile
{
    void ThrowMagicMisile();
}

public interface IThrowFrostNova
{
    void ThrowFrostNova();
}

public interface IRaiseShield
{
    void RaiseShield();
}

public interface IFight
{
    void Fight();
}

public interface IBash
{
    void Bash();
}

public interface ICleave
{
    void Cleave();
}

public interface ISlash
{
    void Slash();
}

public interface IShieldGlare
{
    void ShieldGlare();
}

public class Wizard : ITeleport , IBash,IShieldGlare,ISlash,ICleave,IFight,IRaiseShield,IThrowFrostNova,IThrowMagicMissile,IDie,IHeal
{
    public void Bash()
    {
        throw new NotImplementedException();
    }

    public void Cleave()
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        Console.WriteLine("I'm dying");
    }

    public void Fight()
    {
        Console.WriteLine("I'm fighting");
    }

    public void Heal()
    {
        Console.WriteLine("I'm healing");
    }

    public void RaiseShield()
    {
        throw new NotImplementedException();
    }

    public void ShieldGlare()
    {
        throw new NotImplementedException();
    }

    public void Slash()
    {
        throw new NotImplementedException();
    }

    public void Teleport(int x, int y)
    {
        Console.WriteLine("I'm teleporting to " + x + " " + y);
    }

    public void ThrowFrostNova()
    {
        Console.WriteLine("I'm throwing my frost nova");
    }

    public void ThrowMagicMisile()
    {
        Console.WriteLine("I'm throwing a magic misile");
    }
}




public class Babarian : ITeleport, IBash, IShieldGlare, ISlash, ICleave, IFight, IRaiseShield, IThrowFrostNova, IThrowMagicMissile, IDie, IHeal
{
    public void Bash()
    {
        Console.WriteLine("I'm bashing someone");
    }

    public void Cleave()
    {
        Console.WriteLine("I'm cleaving someone");
    }

    public void Die()
    {
        Console.WriteLine("I'm dying");
    }

    public void Fight()
    {
        Console.WriteLine("I'm fighting");
    }

    public void Heal()
    {
        Console.WriteLine("I'm healing");
    }

    public void RaiseShield()
    {
        throw new NotImplementedException();
    }

    public void ShieldGlare()
    {
        throw new NotImplementedException();
    }

    public void Slash()
    {
        Console.WriteLine("I'm slashing someone");
    }

    public void Teleport(int x, int y)
    {
        throw new NotImplementedException();
    }

    public void ThrowFrostNova()
    {
        throw new NotImplementedException();
    }

    public void ThrowMagicMisile()
    {
        throw new NotImplementedException();
    }
}




public class Knight : ITeleport, IBash, IShieldGlare, ISlash, ICleave, IFight, IRaiseShield, IThrowFrostNova, IThrowMagicMissile, IDie, IHeal
{
    public void Bash()
    {
        Console.WriteLine("I'm bashing someone");
    }

    public void Cleave()
    {
        Console.WriteLine("I'm cleaving someone");
    }

    public void Die()
    {
        Console.WriteLine("I'm dying");
    }

    public void Fight()
    {
        Console.WriteLine("I'm fighting");
    }

    public void Heal()
    {
        Console.WriteLine("I'm healing");
    }

    public void RaiseShield()
    {
        Console.WriteLine("I'm raising my shield");
    }

    public void ShieldGlare()
    {
        Console.WriteLine("I'm throwing shield glare");
    }

    public void Slash()
    {
        Console.WriteLine("I'm slashing someone");
    }

    public void Teleport(int x, int y)
    {
        throw new NotImplementedException();
    }

    public void ThrowFrostNova()
    {
        throw new NotImplementedException();
    }

    public void ThrowMagicMisile()
    {
        throw new NotImplementedException();
    }
}



public class Witch : ITeleport, IBash, IShieldGlare, ISlash, ICleave, IFight, IRaiseShield, IThrowFrostNova, IThrowMagicMissile, IDie, IHeal
{
    public void Bash()
    {
        throw new NotImplementedException();
    }

    public void Cleave()
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        Console.WriteLine("I'm dying");
    }

    public void Fight()
    {
        Console.WriteLine("I'm fighting");
    }

    public void Heal()
    {
        Console.WriteLine("I'm healing");
    }

    public void RaiseShield()
    {
        Console.WriteLine("I'm raising my shield");
    }

    public void ShieldGlare()
    {
        Console.WriteLine("I'm throwing shield glare");
    }

    public void Slash()
    {
        throw new NotImplementedException();
    }

    public void Teleport(int x, int y)
    {
        Console.WriteLine("I'm teleporting to " + x + " " + y);
    }

    public void ThrowFrostNova()
    {
        throw new NotImplementedException();
    }

    public void ThrowMagicMisile()
    {
        throw new NotImplementedException();
    }
}
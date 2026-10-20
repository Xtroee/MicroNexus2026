/// <summary>
/// Damage of the 14 antibiotics against the 7 stage 7-13 bacteria, straight from
/// docs/antibiotic-bacteria-matrix-full-14x14.md (scale 0-20: 0 immune, 20 full effect).
/// The 7 original bacteria keep the damage numbers hard-coded in each projectile class.
/// </summary>
public static class AntibioticDamage
{
    public enum Attacker
    {
        Amoxicillin, Cefixime, Vancomisin, Azitromisin, Ciprofloxacin, Levofloksasin, Cotrimoxazole,
        Meropenem, Ceftriaxone, Linezolid, Clarithromycin, PenicillinV, Colistin, Doxycycline
    }

    public enum Species
    {
        KlebsiellaEsbl, Neisseria, Vre, HPylori, SPyogenes, Crab, Chlamydia
    }

    // rows = Attacker, columns = Species (KlebsiellaEsbl, Neisseria, Vre, HPylori, SPyogenes, Crab, Chlamydia)
    private static readonly int[][] Table =
    {
        /* Amoxicillin    */ new[] {  0,  4,  0, 12, 20,  0,  8 },
        /* Cefixime       */ new[] {  4, 12,  0,  0, 12,  0,  0 },
        /* Vancomisin     */ new[] {  0,  0,  0,  0, 20,  0,  0 },
        /* Azitromisin    */ new[] {  4,  8,  0,  4, 12,  0, 16 },
        /* Ciprofloxacin  */ new[] {  4,  4,  4,  8,  4,  4,  4 },
        /* Levofloksasin  */ new[] {  4,  4,  4, 16, 12,  4, 12 },
        /* Cotrimoxazole  */ new[] {  8,  0,  4,  0,  0,  4,  0 },
        /* Meropenem      */ new[] { 20,  8,  0,  0, 20,  0,  0 },
        /* Ceftriaxone    */ new[] {  4, 20,  0,  0, 20,  0,  0 },
        /* Linezolid      */ new[] {  0,  0, 20,  0, 16,  0,  0 },
        /* Clarithromycin */ new[] {  0,  4,  0, 20, 12,  0,  8 },
        /* Penicillin V   */ new[] {  0,  0,  0,  0, 20,  0,  0 },
        /* Colistin       */ new[] {  8,  0,  0,  0,  0, 20,  0 },
        /* Doxycycline    */ new[] {  4,  4,  4,  8,  4,  4, 20 },
    };

    public static int Get(Attacker attacker, Species species)
    {
        return Table[(int)attacker][(int)species];
    }
}

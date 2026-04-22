public static class DiscountCalculator
{
    public static decimal Calculate(IReadOnlyList<MenuItem> items)
    {
        bool hasSandwich = items.Any(i => i.Type == ProductType.Sandwich);
        bool hasSide     = items.Any(i => i.Type == ProductType.Side);
        bool hasDrink    = items.Any(i => i.Type == ProductType.Drink);

        return (hasSandwich, hasSide, hasDrink) switch
        {
            (true, true, true)   => 0.20m,
            (true, false, true)  => 0.15m,
            (true, true, false)  => 0.10m,
            _                    => 0.00m
        };
    }
}
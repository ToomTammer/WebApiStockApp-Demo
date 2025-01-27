
namespace webapi.Helpers;

public static class PaymentHelper
{
    public static double CalculateFee(double net_total, double fee_pct, double fee_addition)
    {
        return Convert.ToDouble(Math.Round(Convert.ToDecimal(net_total) * Convert.ToDecimal(fee_pct) / 100m, 2, MidpointRounding.AwayFromZero)) + fee_addition;
    }

    public static double CalculateVatFee(this double payment_fee, double? vat_pct = null)
    {
        vat_pct = vat_pct ?? 3;
        double vatFee = Convert.ToDouble(Math.Round(Convert.ToDecimal(payment_fee) * Convert.ToDecimal(vat_pct) / 100m, 2, MidpointRounding.AwayFromZero));
        return payment_fee + vatFee;
    }

    public static double CalculateForeignExchangeRateTotal(double total, double fx_rate)
    {
        return Convert.ToDouble(Math.Round(Convert.ToDecimal(total) * Convert.ToDecimal(fx_rate), 2, MidpointRounding.AwayFromZero));
    }

    public static double CalculateVatFromIncludeVat(double total, double vat_pct)
    {
        return total - CalculateExcludeVatFromIncludeVat(total, vat_pct);
    }

    public static double CalculateExcludeVatFromIncludeVat(double total, double vat_pct)
    {
        return Convert.ToDouble(Math.Round(Convert.ToDecimal(total) * (100m / (100m + Convert.ToDecimal(vat_pct))), 2, MidpointRounding.AwayFromZero));
    }

    public static double CalculateVatFromExcludeVat(double total, double vat_pct)
    {
        return Convert.ToDouble(Math.Round(Convert.ToDecimal(total) * Convert.ToDecimal(vat_pct) / 100m, 2, MidpointRounding.AwayFromZero));
    }

    public static double CalculateWithholdingTax(double total_excl_vat, double wht_pct)
    {
        return Convert.ToDouble(Math.Round(Convert.ToDecimal(total_excl_vat) * Convert.ToDecimal(wht_pct) / 100m, 2, MidpointRounding.AwayFromZero));
    }

    public static double CalculateNetWithholdingTax(double net_total, double wht)
    {
        return net_total - wht;
    }

    public static double CalculateDiscountPercent(double price, double percent)
    {
        return Convert.ToDouble(Math.Round(Convert.ToDecimal(price) * Convert.ToDecimal(percent) / 100m, 2, MidpointRounding.AwayFromZero));
    }
    public static double CalculateNetTotal(this double net_total, double addition)
    {
        return net_total + addition;
    }

    public static double CalculateDiff(this double first_num, double second_num)
    {
        return Math.Round(first_num, 2, MidpointRounding.AwayFromZero) - Math.Round(second_num, 2, MidpointRounding.AwayFromZero);
    }

     public static double CalculateRemainValue(this double first_num, double second_num)
    {
        return first_num - second_num;
	}

}
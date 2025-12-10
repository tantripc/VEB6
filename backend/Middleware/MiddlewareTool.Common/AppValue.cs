using System.Net;
using System.Text.RegularExpressions;
using static MiddlewareTool.Common.AppType;

namespace MiddlewareTool.Common
{
    public sealed class AppValue
    {
        public const string Version = "5.5";
        private const string Pattern = @"\p{IsCombiningDiacriticalMarks}+";
        public const string Key_Setting_RefundCOD_AllowB2B = @"RefundCOD_AllowB2B";
        /// <summary>
        /// TempFolder
        /// </summary>
        public const string TempFolder_Key = "TempFolder";
        /// <summary>
        /// C:\\TempFolder
        /// </summary>
        public const string TempFolder_Default = "C:\\TempFolder";
        public static string ToUnsignString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            input = input.ToLower().Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(Pattern);
            string str = input.Normalize(System.Text.NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }
        public enum ActiveFlag : byte
        {
            Active = 0,
            Deactive = 1,
            Delete = 2
        }
        public enum Action : byte
        {
            List = 0,
            Insert = 1,
            Update = 2,
            ChangeActive = 3,
            Delete = 4,
            Detail = 5,
        }
        #region Sale order

        public enum SaleOrderAction : int
        {
            List = 0,
            Insert = 1,
            Update = 2,
            ChangeActive = 3,
            Delete = 4,
            Detail = 5,
            SendRequest = 6,
            Approve = 7,
            Invoice = 8,
            Reject = 9
        }
        public enum SaleOrderStatuses : byte
        {
            Updated = 0,
            Waiting = 1,
            Approved = 2,
            Invoiced = 3,
            Rejected = 4
        }

        public class InvoiceTypeCodes
        {
            public const string VATInvoice = "01";
            public const string SalesInvoice = "02";
            public const string OrganizationsAndIndividualsInNon_tariffAreas = "04";
        }

        public enum AdjustmentTypes : int
        {
            OriginalBill = 1,
            InvoiceReplacement = 3,
            InvoiceAdjustment = 5,
            InvoiceCancellation = 7,
            CommercialDiscountInvoice = 9
        }

        public class PaymentMethodCodes
        {
            public const string Cash = "01";
            public const string BankTransfer = "02";
            public const string Credit = "03";
            public const string Cash_BankTransfer = "04";
        }

        public enum AdjustmentStatuses : int
        {
            Gain = 1,
            Decrease = -1
        }

        #endregion

        public static string ProductTypeToString(ProductType type)
        {
            if (type == null)
                type = ProductType.Consignment;
            switch (type)
            {
                case ProductType.ConsignmentEC:
                    return "Consighment EC";
                default:
                    return type.ToString();
            }
        }

        public static string HtmlEncode(string input, bool isAscii = false)
        {
            if (isAscii)
            {
                //string[] decode = { "&quot;", "&amp;", "&lt;", "&gt;", "&euro;", "&Aacute;", "&aacute;", "&Acirc;", "&acirc;", "&acute;", "&AElig;", "&aelig;", "&Agrave;", "&agrave;", "&Aring;", "&aring;", "&Atilde;", "&atilde;", "&Auml;", "&auml;", "&brvbar;", "&Ccedil;", "&ccedil;", "&cedil;", "&cent;", "&circ;", "&copy;", "&curren;", "&deg;", "&divide;", "&Eacute;", "&eacute;", "&Ecirc;", "&ecirc;", "&Egrave;", "&egrave;", "&ETH;", "&eth;", "&Euml;", "&euml;", "&euro;", "&fnof;", "&frac12;", "&frac14;", "&frac34;", "&Iacute;", "&iacute;", "&Icirc;", "&icirc;", "&iexcl;", "&Igrave;", "&igrave;", "&iquest;", "&Iuml;", "&iuml;", "&laquo;", "&macr;", "&micro;", "&middot;", "&not;", "&Ntilde;", "&ntilde;", "&Oacute;", "&oacute;", "&Ocirc;", "&ocirc;", "&OElig;", "&oelig;", "&Ograve;", "&ograve;", "&ordf;", "&ordm;", "&Oslash;", "&oslash;", "&Otilde;", "&otilde;", "&Ouml;", "&ouml;", "&para;", "&plusmn;", "&pound;", "&raquo;", "&reg;", "&Scaron;", "&scaron;", "&sect;", "&shy;", "&sup1;", "&sup2;", "&sup3;", "&szlig;", "&THORN;", "&thorn;", "&tilde;", "&times;", "&Uacute;", "&uacute;", "&Ucirc;", "&ucirc;", "&Ugrave;", "&ugrave;", "&uml;", "&Uuml;", "&uuml;", "&Yacute;", "&yacute;", "&yen;", "&Yuml;", "&yuml;", "&ndash;", "&mdash;", "&lsquo;", "&rsquo;", "&sbquo;", "&ldquo", "&rdquo;", "&bdquo;", "&lsaquo;", "&rsaquo", "&dagger;", "&Dagger;", "&permil;", "&bull;", "&hellip;", "&Prime;", "&prime;", "&oline;", "&frasl;", "&weierp;", "&image;", "&real;", "&trade;", "&alefsym;", "&larr;", "&uarr;", "&rarr;", "&darr;", "&harr;", "&crarr;", "&lArr;", "&uArr;", "&rArr;", "&dArr;", "&hArr;", "&forall;", "&part;", "&exist;", "&empty;", "&nabla;", "&isin;", "&notin;", "&ni;", "&prod;", "&sum;", "&minus;", "&lowast;", "&radic;", "&prop;", "&infin;", "&ang;", "&and;", "&or;", "&cap;", "&cup;", "&int;", "&there4;", "&sim;", "&cong;", "&asymp;", "&ne;", "&equiv;", "&le;", "&ge;", "&sub;", "&sup;", "&nsub;", "&sube;", "&supe;", "&oplus;", "&otimes;", "&perp;", "&hArr;", "&sdot;", "&lceil;", "&rceil;", "&lfloor;", "&rfloor;", "&lang;", "&rang;", "&loz;", "&spades;", "&clubs;", "&hearts;", "&diams;", "&Alpha;", "&alpha;", "&Beta;", "&beta;", "&Gamma;", "&gamma;", "&Delta;", "&delta;", "&Epsilon;", "&epsilon;", "&Zeta;", "&zeta;", "&Eta;", "&eta;", "&Theta;", "&theta;", "&thetasym;", "&Iota;", "&iota;", "&Kappa;", "&kappa;", "&Lambda;", "&lambda;", "&Mu;", "&mu;", "&Nu;", "&nu;", "&Xi;", "&xi;", "&Omicron;", "&omicron;", "&Pi;", "&pi;", "&piv;", "&Rho;", "&rho;", "&Sigma;", "&sigma;", "&sigmaf;", "&Tau;", "&tau;", "&Upsilon;", "&upsilon;", "&upsih;", "&Phi;", "&phi;", "&Chi;", "&chi;", "&Psi;", "&psi;", "&Omega;", "&omega;" };
                string[] decode = { "&lang;", "&rang;", "&ndash;", "&mdash;", "&quot;", "&amp;", "&circ;", "&iexcl;", "&brvbar;", "&uml;", "&macr;", "&acute;", "&cedil;", "&iquest;", "&tilde;", "&lsquo;", "&rsquo;", "&sbquo;", "&ldquo;", "&rdquo;", "&bdquo;", "&prime;", "&Prime;", "&lsaquo;", "&rsaquo;", "&oline;", "&oplus;", "&minus;", "&otimes;", "&frasl;", "&lowast;", "&lt;", "&gt;", "&plusmn;", "&laquo;", "&raquo;", "&times;", "&divide;", "&forall;", "&part;", "&exist;", "&empty;", "&nabla;", "&isin;", "&notin;", "&ni;", "&prod;", "&sum;", "&radic;", "&prop;", "&ang;", "&and;", "&or;", "&cap;", "&cup;", "&int;", "&there4;", "&sim;", "&cong;", "&asymp;", "&ne;", "&equiv;", "&le;", "&ge;", "&sub;", "&sup;", "&nsub;", "&sube;", "&supe;", "&perp;", "&sdot;", "&lceil;", "&rceil;", "&lfloor;", "&rfloor;", "&loz;", "&uarr;", "&uArr;", "&rarr;", "&lArr;", "&rArr;", "&darr;", "&dArr;", "&larr;", "&crarr;", "&harr;", "&hArr;", "&hArr;", "&cent;", "&pound;", "&curren;", "&yen;", "&sect;", "&copy;", "&not;", "&reg;", "&deg;", "&micro;", "&para;", "&middot;", "&dagger;", "&Dagger;", "&bull;", "&hellip;", "&permil;", "&spades;", "&clubs;", "&hearts;", "&diams;", "&euro;", "&euro;", "&frac14;", "&frac12;", "&frac34;", "&sup1;", "&sup2;", "&sup3;", "&infin;", "&ordf;", "&Aacute;", "&aacute;", "&Agrave;", "&agrave;", "&Acirc;", "&acirc;", "&Auml;", "&auml;", "&Atilde;", "&atilde;", "&Aring;", "&aring;", "&AElig;", "&aelig;", "&Ccedil;", "&ccedil;", "&ETH;", "&eth;", "&Eacute;", "&eacute;", "&Egrave;", "&egrave;", "&Ecirc;", "&ecirc;", "&Euml;", "&euml;", "&fnof;", "&image;", "&Iacute;", "&iacute;", "&Igrave;", "&igrave;", "&Icirc;", "&icirc;", "&Iuml;", "&iuml;", "&Ntilde;", "&ntilde;", "&ordm;", "&Oacute;", "&oacute;", "&Ograve;", "&ograve;", "&Ocirc;", "&ocirc;", "&Ouml;", "&ouml;", "&Otilde;", "&otilde;", "&Oslash;", "&oslash;", "&OElig;", "&oelig;", "&weierp;", "&real;", "&Scaron;", "&scaron;", "&szlig;", "&THORN;", "&thorn;", "&trade;", "&Uacute;", "&uacute;", "&Ugrave;", "&ugrave;", "&Ucirc;", "&ucirc;", "&Uuml;", "&uuml;", "&Yacute;", "&yacute;", "&Yuml;", "&yuml;", "&Alpha;", "&alpha;", "&Beta;", "&beta;", "&Gamma;", "&gamma;", "&Delta;", "&delta;", "&Epsilon;", "&epsilon;", "&Zeta;", "&zeta;", "&Eta;", "&eta;", "&Theta;", "&theta;", "&thetasym;", "&Iota;", "&iota;", "&Kappa;", "&kappa;", "&Lambda;", "&lambda;", "&Mu;", "&mu;", "&Nu;", "&nu;", "&Xi;", "&xi;", "&Omicron;", "&omicron;", "&Pi;", "&pi;", "&piv;", "&Rho;", "&rho;", "&Sigma;", "&sigma;", "&sigmaf;", "&Tau;", "&tau;", "&Upsilon;", "&upsilon;", "&upsih;", "&Phi;", "&phi;", "&Chi;", "&chi;", "&Psi;", "&psi;", "&Omega;", "&omega;", "&alefsym;" };
                char[] encode = { '⟨', '⟩', '–', '—', '"', '&', 'ˆ', '¡', '¦', '¨', '¯', '´', '¸', '¿', '˜', '‘', '’', '‚', '“', '”', '„', '′', '″', '‹', '›', '‾', '⊕', '−', '⊗', '⁄', '∗', '<', '>', '±', '«', '»', '×', '÷', '∀', '∂', '∃', '∅', '∇', '∈', '∉', '∋', '∏', '∑', '√', '∝', '∠', '∧', '∨', '∩', '∪', '∫', '∴', '∼', '≅', '≈', '≠', '≡', '≤', '≥', '⊂', '⊃', '⊄', '⊆', '⊇', '⊥', '⋅', '⌈', '⌉', '⌊', '⌋', '◊', '↑', '⇑', '→', '⇐', '⇒', '↓', '⇓', '←', '↵', '↔', '⇔', '⇔', '¢', '£', '¤', '¥', '§', '©', '¬', '®', '°', 'µ', '¶', '·', '†', '‡', '•', '…', '‰', '♠', '♣', '♥', '♦', '€', '€', '¼', '½', '¾', '¹', '²', '³', '∞', 'ª', 'Á', 'á', 'À', 'à', 'Â', 'â', 'Ä', 'ä', 'Ã', 'ã', 'Å', 'å', 'Æ', 'æ', 'Ç', 'ç', 'Ð', 'ð', 'É', 'é', 'È', 'è', 'Ê', 'ê', 'Ë', 'ë', 'ƒ', 'ℑ', 'Í', 'í', 'Ì', 'ì', 'Î', 'î', 'Ï', 'ï', 'Ñ', 'ñ', 'º', 'Ó', 'ó', 'Ò', 'ò', 'Ô', 'ô', 'Ö', 'ö', 'Õ', 'õ', 'Ø', 'ø', 'Œ', 'œ', '℘', 'ℜ', 'Š', 'š', 'ß', 'Þ', 'þ', '™', 'Ú', 'ú', 'Ù', 'ù', 'Û', 'û', 'Ü', 'ü', 'Ý', 'ý', 'Ÿ', 'ÿ', 'Α', 'α', 'Β', 'β', 'Γ', 'γ', 'Δ', 'δ', 'Ε', 'ε', 'Ζ', 'ζ', 'Η', 'η', 'Θ', 'θ', 'ϑ', 'Ι', 'ι', 'Κ', 'κ', 'Λ', 'λ', 'Μ', 'μ', 'Ν', 'ν', 'Ξ', 'ξ', 'Ο', 'ο', 'Π', 'π', 'ϖ', 'Ρ', 'ρ', 'Σ', 'σ', 'ς', 'Τ', 'τ', 'Υ', 'υ', 'ϒ', 'Φ', 'φ', 'Χ', 'χ', 'Ψ', 'ψ', 'Ω', 'ω', 'ℵ' };
                string output = "";
                for (var i = 0; i < input.Length; i++)
                {
                    if (encode.Contains(input[i]))
                    {
                        var encodeIndex = Array.FindIndex(encode, x => x.Equals(input[i]));
                        output += decode[encodeIndex];
                    }
                    else
                        output += input[i];
                }
                return output;
            }
            return WebUtility.HtmlEncode(input);
        }

        public static string GuidToUUID(Guid id)
        {
            return id.ToString("N");
        }
        public static Guid UUIDToGuid(string uuid)
        {
            if (string.IsNullOrEmpty(uuid) || uuid.Length != 32)
                return Guid.Empty;
            return new Guid(uuid);
        }
    }
}

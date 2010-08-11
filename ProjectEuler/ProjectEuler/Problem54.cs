using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;
using Developpez.Dotnet.IO;
using Developpez.Dotnet.Collections;
using System.IO;

namespace ProjectEuler
{
    class Problem54 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            string path = @"Data\p54_poker.txt";
            //string path = @"Data\p54_poker_test.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.AsLineEnumerable()
                    .Select(ReadHands)
                    .Where(Player1Wins)
                    .Count();
            }
        }

        private bool Player1Wins(Tuple<PokerHand, PokerHand> hands)
        {
            bool player1Wins = hands.Item1.CompareTo(hands.Item2) >= 0;
            return player1Wins;
        }

        private Tuple<PokerHand, PokerHand> ReadHands(string line)
        {
            string sCards1 = line.Substring(0, 14).Trim();
            string sCards2 = line.Substring(15).Trim();
            var cards1 = sCards1.Split(' ').Select(s => PokerCard.Parse(s));
            var cards2 = sCards2.Split(' ').Select(s => PokerCard.Parse(s));
            return Tuple.Create(new PokerHand(cards1), new PokerHand(cards2));
        }

        #endregion
    }

    struct PokerCard : IComparable<PokerCard>
    {
        public PokerCard(int value, PokerSuit suit)
            : this()
        {
            this.Value = value;
            this.Suit = suit;
        }

        public int Value { get; private set; }
        public PokerSuit Suit { get; private set; }

        #region IComparable<PokerCard> Members

        public int CompareTo(PokerCard other)
        {
            return _cardValueComparer.Compare(this.Value, other.Value);
        }

        #endregion

        private static readonly IComparer<int> _cardValueComparer = new PokerCardValueComparer();
        public static IComparer<int> CardValueComparer
        {
            get { return _cardValueComparer; }
        }


        private class PokerCardValueComparer : IComparer<int>
        {

            #region IComparer<int> Members

            public int Compare(int x, int y)
            {
                if (x == 1 && y != 1)
                    return 1;
                else if (x != 1 && y == 1)
                    return -1;
                else
                    return x.CompareTo(y);
            }

            #endregion
        }

        public static char FormatValue(int value)
        {
            return value.Switch<int, char>()
                            .Case(1, 'A')
                            .Case(10, 'T')
                            .Case(11, 'J')
                            .Case(12, 'Q')
                            .Case(13, 'K')
                            .Else(v => v.ToString()[0]);
        }

        public static PokerCard Parse(string s)
        {
            char cValue = s[0];
            char cSuit = s[1];

            int value = cValue.Switch<char, int>()
                        .Case('T', 10)
                        .Case('J', 11)
                        .Case('Q', 12)
                        .Case('K', 13)
                        .Case('A', 1)
                        .Case(c => char.IsDigit(c), c => int.Parse(c.ToString()))
                        .ElseThrow("Valeur de carte incorrecte");

            PokerSuit suit = cSuit.Switch<char, PokerSuit>()
                            .Case('S', PokerSuit.Spades)
                            .Case('H', PokerSuit.Hearts)
                            .Case('D', PokerSuit.Diamonds)
                            .Case('C', PokerSuit.Clubs)
                            .ElseThrow("Couleur de carte incorrecte");

            return new PokerCard(value, suit);
        }

        public override string ToString()
        {
            char cValue = FormatValue(Value);
            char cSuit = Suit.ToString()[0];
            return string.Format("{0}{1}", cValue, cSuit);
        }
    }

    enum PokerSuit
    {
        Spades,
        Hearts,
        Diamonds,
        Clubs
    }

    struct PokerHand : IComparable<PokerHand>
    {
        private PokerCard[] _cards;

        public PokerHand(IEnumerable<PokerCard> cards)
            : this()
        {
            _cards = cards.OrderByDescending(c => c.Value, PokerCard.CardValueComparer).ToArray();
            FindCombination();
        }

        private void FindCombination()
        {
            bool isStraight = IsStraight();
            bool isFlush = IsFlush();

            if (isStraight)
            {
                if (isFlush)
                {
                    if (_cards[0].Value == 1)
                    {
                        Value1 = _cards[0].Value;
                        Value2 = _cards[1].Value;
                        Combination = PokerCombination.RoyalFlush;
                        return;
                    }
                    else
                    {
                        Value1 = _cards[0].Value;
                        Value2 = _cards[1].Value;
                        Combination = PokerCombination.StraightFlush;
                        return;
                    }
                }
                Value1 = _cards[0].Value;
                Value2 = _cards[1].Value;
                Combination = PokerCombination.Straight;
                return;
            }

            if (isFlush)
            {
                Value1 = _cards[0].Value;
                Value2 = _cards[1].Value;
                Combination = PokerCombination.Flush;
                return;
            }

            var grouped = _cards
                            .GroupBy(c => c.Value)
                            .OrderByDescending(g => g.Count())
                            .ThenByDescending(g => g.Key, PokerCard.CardValueComparer)
                            .ToArray();

            if (grouped[0].Count() == 4)
            {
                Value1 = grouped[0].Key;
                Value2 = grouped[1].Key;
                Combination = PokerCombination.FourOfAKind;
                return;
            }

            if (grouped[0].Count() == 3)
            {
                if (grouped[1].Count() == 2)
                {
                    Value1 = grouped[0].Key;
                    Value2 = grouped[1].Key;
                    Combination = PokerCombination.FullHouse;
                    return;
                }
                else
                {
                    Value1 = grouped[0].Key;
                    Value2 = grouped[1].Key;
                    Combination = PokerCombination.ThreeOfAKind;
                    return;
                }
            }

            if (grouped[0].Count() == 2)
            {
                if (grouped[1].Count() == 2)
                {
                    Value1 = grouped[0].Key;
                    Value2 = grouped[1].Key;
                    Combination = PokerCombination.TwoPairs;
                    return;
                }
                else
                {
                    Value1 = grouped[0].Key;
                    Value2 = grouped[1].Key;
                    Combination = PokerCombination.OnePair;
                    return;
                }
            }

            Value1 = _cards[0].Value;
            Value2 = _cards[1].Value;
            Combination = PokerCombination.HighCard;
            return;
        }

        private bool IsFlush()
        {
            PokerSuit suit = (PokerSuit)(- 1);
            foreach (var card in _cards)
            {
                if ((int)suit == -1)
                    suit = card.Suit;
                else if (card.Suit != suit)
                    return false;
            }
            return true;
        }

        private bool IsStraight()
        {
            int prev = 0;
            foreach (var card in _cards)
            {
                int v = card.Value;
                if (v == 1) v = 14;
                if (prev != 0)
                {
                    if (v != prev - 1)
                        return false;
                }
                prev = v;
            }
            return true;
        }

        public IEnumerable<PokerCard> Cards
        {
            get { return _cards; }
        }
        public PokerCombination Combination { get; private set; }
        public int Value1 { get; private set; }
        public int Value2 { get; private set; }

        #region IComparable<PokerHand> Members

        public int CompareTo(PokerHand other)
        {
            int comp = this.Combination.CompareTo(other.Combination);
            if (comp == 0)
                comp = PokerCard.CardValueComparer.Compare(this.Value1, other.Value1);
            if (comp == 0)
                comp = PokerCard.CardValueComparer.Compare(this.Value2, other.Value2);
            if (comp == 0)
            {
                comp = this.Cards
                    .Zip(other.Cards, (c1, c2) => PokerCard.CardValueComparer.Compare(c1.Value, c2.Value))
                    .FirstOrDefault(x => x != 0);
            }
            return comp;
        }

        #endregion

        public override string ToString()
        {
            return Cards.Select(c => c.ToString()).Join(" ");
        }

        public string RankedHand
        {
            get
            {
                return string.Format("{0} : {1}, {2}",
                    Combination,
                    PokerCard.FormatValue(Value1),
                    PokerCard.FormatValue(Value2));
            }
        }
    }

    enum PokerCombination
    {
        HighCard,
        OnePair,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }
}

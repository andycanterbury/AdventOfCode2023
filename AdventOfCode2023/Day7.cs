using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace AdventOfCode2023
{
    public class Day7
    {
        public static string[] fileData;

        [SetUp]
        public void Setup()
        {
            fileData = Utilities.InputLoader.LoadFile("Data/Day7.txt");
        }

        [Test]
        public void Part1()
        {
            var handList = new List<Hand>();

            foreach (var hand in fileData)
            {
                var cards = new List<int>();
                var data = hand.Split(' ');
                foreach (var card in data[0])
                {
                    var cardVal = 0;
                    switch (card)
                    {
                        case 'A':
                            cardVal = 14;
                            break;
                        case 'K':
                            cardVal = 13;
                            break;
                        case 'Q':
                            cardVal = 12;
                            break;
                        case 'J':
                            cardVal = 11;
                            break;
                        case 'T':
                            cardVal = 10;
                            break;
                        default:
                            cardVal = int.Parse(card.ToString());
                            break;
                    }
                    cards.Add(cardVal);
                }
                handList.Add(new Hand { Cards = cards, UnProcessedCards = data[0], Bid = int.Parse(data[1]), Type = HandType.NotScored });
            }

            handList = ScoreHands(handList);

            var orderedHandList = handList.OrderByDescending(h => h);
            long winnings = 0;
            var rank = orderedHandList.Count();

            using (var streamWriter = new StreamWriter("Data/Day7Out.txt"))
            {
                foreach (var hand in orderedHandList)
                {
                    winnings += (hand.Bid * rank);
                    streamWriter.WriteLine($"{hand.Type} {hand.UnProcessedCards} {hand.Bid} {rank}");
                    rank--;
                }
                Console.WriteLine(winnings);
            }
        }

        [Test]
        public void Part2()
        {
            var handList = new List<Hand>();

            foreach (var hand in fileData)
            {
                var cards = new List<int>();
                var data = hand.Split(' ');
                foreach (var card in data[0])
                {
                    var cardVal = 0;
                    switch (card)
                    {
                        case 'A':
                            cardVal = 14;
                            break;
                        case 'K':
                            cardVal = 13;
                            break;
                        case 'Q':
                            cardVal = 12;
                            break;
                        case 'J':
                            cardVal = 1;
                            break;
                        case 'T':
                            cardVal = 10;
                            break;
                        default:
                            cardVal = int.Parse(card.ToString());
                            break;
                    }
                    cards.Add(cardVal);
                }
                handList.Add(new Hand { Cards = cards, UnProcessedCards = data[0], Bid = int.Parse(data[1]), Type = HandType.NotScored });
            }

            handList = ScoreHands(handList, true);

            var orderedHandList = handList.OrderByDescending(h => h);
            long winnings = 0;
            var rank = orderedHandList.Count();

            using (var streamWriter = new StreamWriter("Data/Day7Out.txt"))
            {
                foreach (var hand in orderedHandList)
                {
                    winnings += (hand.Bid * rank);
                    streamWriter.WriteLine($"{hand.Type} {hand.UnProcessedCards} {hand.Bid} {rank}");
                    rank--;
                }
                Console.WriteLine(winnings);
            }
        }


        public List<Hand> ScoreHands(List<Hand> handList, bool jokersWild = false)
        {
            foreach(var hand in handList)
            {
                var distinctCards = hand.Cards.Distinct();
                foreach(var card in distinctCards)
                {
                    if (jokersWild && card == 1) continue;
                    var count = hand.Cards.Count(c => c == card);
                    switch (count)
                    {
                        case 5:
                            hand.Type = HandType.FiveOfAKind;
                            break;
                        case 4:
                            hand.Type = HandType.FourOfAKind; 
                            break;
                        case 3:
                            if (hand.Type == HandType.OnePair)
                            {
                                hand.Type = HandType.FullHouse;
                            } else
                            {
                                if (hand.Type < HandType.ThreeOfAKind)
                                {
                                    hand.Type = HandType.ThreeOfAKind;
                                }
                            }
                            break;
                        case 2:
                            if (hand.Type == HandType.ThreeOfAKind)
                            {
                                hand.Type = HandType.FullHouse;
                            }
                            else if (hand.Type == HandType.OnePair)
                            {
                                hand.Type = HandType.TwoPair;
                            }
                            else 
                            {
                                if (hand.Type < HandType.OnePair)
                                {
                                    hand.Type = HandType.OnePair;
                                }
                            }
                            break;
                        case 1:
                            if (hand.Type == HandType.NotScored)
                            {
                                hand.Type = HandType.HighCard;
                            }
                            break;
                    }
                }
                
                if(jokersWild)
                {
                    var jokerCount = hand.Cards.Count(c => c == 1);
                    if (jokerCount > 0)
                    {
                        for(int j = 1; j <= jokerCount; j++) 
                        {
                            if (hand.Type == HandType.NotScored)
                            {
                                hand.Type = HandType.HighCard;
                                continue;
                            }
                            if (hand.Type == HandType.HighCard) 
                            { 
                                hand.Type = HandType.OnePair; 
                                continue; 
                            }
                            if (hand.Type == HandType.OnePair) 
                            { 
                                hand.Type = HandType.ThreeOfAKind; 
                                continue; 
                            }
                            if (hand.Type == HandType.TwoPair) 
                            { 
                                hand.Type = HandType.FullHouse; 
                                continue; 
                            }
                            if (hand.Type == HandType.ThreeOfAKind) 
                            { 
                                hand.Type = HandType.FourOfAKind; 
                                continue; 
                            }
                            if (hand.Type == HandType.FullHouse)
                            {
                                hand.Type = HandType.FourOfAKind;
                                continue;
                            }
                            if (hand.Type == HandType.FourOfAKind)
                            {
                                hand.Type = HandType.FiveOfAKind;
                                continue;
                            }
                        }
                    }
                }
            }
            return handList;
        }

        public class Hand : IComparable
        {
            public List<int> Cards { get; set; }
            public string UnProcessedCards { get; set; }
            public int Bid { get; set; }
            public HandType Type { get; set; }

            public int CompareTo(object? obj)
            {
                var otherHand = obj as Hand;
                //samesies
                if (this.Cards == otherHand.Cards) return 0;

                //Order based on type
                if (this.Type > otherHand.Type) return 1;
                if (this.Type < otherHand.Type) return -1;

                //Same type, order based on best first cards
                if (this.Type == otherHand.Type)
                {
                    for(int i = 0; i< this.Cards.Count; i++) 
                    {
                        if (this.Cards[i] > otherHand.Cards[i]) return 1;
                        if (this.Cards[i] < otherHand.Cards[i]) return -1;
                    }
                }
                return 0;
            }
        }

        public enum HandType
        {
            FiveOfAKind = 7,
            FourOfAKind = 6,
            FullHouse = 5,
            ThreeOfAKind = 4,
            TwoPair = 3,
            OnePair = 2,
            HighCard = 1,
            NotScored = 0
        }

    }

}

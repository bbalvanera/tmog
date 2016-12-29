using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMog.Entities;

namespace TMog.Business
{
    public class SlotManager
    {
        private readonly string[] slots;

        public SlotManager()
        {
            slots = Enumerable.Repeat<string>("X", 11).ToArray();
        }

        public SlotManager(string slots)
        {
            if (string.IsNullOrWhiteSpace(slots))
            {
                throw new ArgumentNullException(nameof(slots));
            }

            if (slots.Length > GetNumberOfItems())
            {
                throw new ArgumentException("Invalid slot length");
            }

            if (slots.Any(s => !Regex.IsMatch(s.ToString(), "[01X]")))
            {
                throw new ArgumentException("Invalid slot string");
            }

            this.slots = slots.Select(s => s.ToString()).ToArray();
        }

        public bool this[SlotType slot]
        {
            get { return slots[(int)slot] != "0"; }
            set { slots[(int)slot] = value ? "1" : "0"; }
        }

        public bool ContainsSlot(SlotType slot)
        {
            return slots[(int)slot] != "X";
        }

        public bool IsComplete(SlotType slot)
        {
            return this[slot];
        }

        public int ActiveSlotCount
        {
            get { return slots.Count(s => s != "X"); }
        }

        public int CompletedSlotCount
        {
            get { return slots.Count(s => s == "1"); }
        }

        public void Mark(SlotType slot, bool complete)
        {
            this[slot] = complete;
        }

        public override string ToString()
        {
            return string.Join("", this.slots);
        }

        public static string FromRange(IEnumerable<int> slots)
        {
            if (slots == null)
            {
                throw new ArgumentNullException(nameof(slots));
            }

            var returnValue = GetDefault();
            foreach (var slot in slots)
            {
                returnValue[slot] = "0";
            }

            return string.Join("", returnValue);
        }

        private static string[] GetDefault()
        {
            var numberOfItems = GetNumberOfItems();
            return Enumerable.Repeat<string>("X", numberOfItems).ToArray();
        }

        private static int GetNumberOfItems()
        {
            return Enum.GetValues(typeof(SlotType)).Length;
        }
    }
}

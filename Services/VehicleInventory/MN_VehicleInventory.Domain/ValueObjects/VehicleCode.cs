using MN_VehicleInventory.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MN_VehicleInventory.Domain.ValueObjects {
    public record VehicleCode {
        public string Value { get; set; }
        public VehicleCode(string value) {
            if (string.IsNullOrEmpty(value))
                throw new MN_DomainException("Vehicle Code cannot be null or empty.");

            Value = value;
        }
        public override string ToString() => Value;
    }
}

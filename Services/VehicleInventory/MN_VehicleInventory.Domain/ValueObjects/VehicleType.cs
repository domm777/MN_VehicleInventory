using MN_VehicleInventory.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MN_VehicleInventory.Domain.ValueObjects {
    public record VehicleType {
        public string Value { get; }
        public VehicleType(string value) {
            if (string.IsNullOrWhiteSpace(value)) 
                throw new MN_DomainException("Vehicle Type cannot be null or empty.");

            Value = value;
        }
        public override string ToString() => Value;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseOptimizer.Domain.Models;

public record PlacementResult(SkuRecord Sku, WarehouseCell Cell);
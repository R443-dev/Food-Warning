﻿using FoodWarning.Portable.Interfaces;

namespace FoodWarning.Portable.Helpers
{
  /// <summary>
  /// Static helper class to set the instance of IMessage
  /// </summary>
  public static class CrossPlatformMessage
  {
    public static IMessage Instance { get; set; }
  }
}

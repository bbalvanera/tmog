import { Injectable } from '@angular/core';

@Injectable()
export class WowheadTooltipService {
  public dismissTooltip(): void {
    if (WH && WH.Tooltip) {
      if (WH.Tooltip.showingTooltip) {
        WH.Tooltip.hide();
      }
    }
  }
}

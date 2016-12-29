import { Injectable } from '@angular/core';

@Injectable()
export class WowheadTooltipService {
    public dismissTooltip(): void {
        // this is necessary because, the tooltip won't hide after transition.
        if ($WH && $WH.Tooltip) {
            if ($WH.Tooltip.showingTooltip) {
                $WH.Tooltip.hide();
            }
        }
    }
}

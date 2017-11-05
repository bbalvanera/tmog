declare var WH: {
    Tooltip: {
        showingTooltip: boolean,
        hide(): void
    }
}; // Enables access to global variable set by wowhead.com plugin. See below.

/*
    Each link in page is set so the wowhead.com plugin would show a tooltip showing what the set looks like.
    However, this tooltip only hides on mouseout event which is never triggered when changing states (navigating)
    A reference is required to manullay hide the tooltip after state transition.
*/
declare var $WowheadPower: {
    refreshLinks(): void;
};

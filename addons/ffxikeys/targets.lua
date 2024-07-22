local resources = require('resources')

local targets = {}

targets[resources.zones:with('en', 'Port San d\'Oria').id] =
{
    name = 'Habitox'
}
targets[resources.zones:with('en', 'Southern San d\'Oria').id] =
{
    name = 'Mystrix'
}
targets[resources.zones:with('en', 'Bastok Mines').id] =
{
    name = 'Bountibox'
}
targets[resources.zones:with('en', 'Bastok Markets').id] =
{
    name = 'Specilox'
}
targets[resources.zones:with('en', 'Windurst Walls').id] =
{
    name = 'Arbitrix'
}
targets[resources.zones:with('en', 'Windurst Woods').id] =
{
    name = 'Funtrox'
}
targets[resources.zones:with('en', 'Lower Jeuno').id] =
{
    name = 'Sweepstox'
}
targets[resources.zones:with('en', 'Upper Jeuno').id] =
{
    name = 'Priztrix'
}
targets[resources.zones:with('en', 'Aht Urhgan Whitegate').id] =
{
    name = 'Wondrix'
}
targets[resources.zones:with('en', 'Western Adoulin').id] =
{
    name = 'Rewardox'
}
targets[resources.zones:with('en', 'Eastern Adoulin').id] =
{
    name = 'Winrix'
}

return targets
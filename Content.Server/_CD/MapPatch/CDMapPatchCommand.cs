﻿/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 * This Source Code Form is "Incompatible With Secondary Licenses", as
 * defined by the Mozilla Public License, v. 2.0.
 */

using Content.Server.Administration;
using Content.Shared.Administration;
using Robust.Shared.ContentPack;
using Robust.Shared.Toolshed;
using Robust.Shared.Utility;

namespace Content.Server._CD.MapPatch;

[ToolshedCommand(Name = "cd_map_patch"), AdminCommand(AdminFlags.Mapping)]
public sealed class CDMapPatchCommand : ToolshedCommand
{
    [Dependency] private readonly IResourceManager _res = default!;
    private MapPatchSystem? _mapPatch = default;

    [CommandImplementation("print")]
    public string Print()
    {
        _mapPatch ??= GetSys<MapPatchSystem>();

        return _mapPatch.PatchToText();
    }

    [CommandImplementation("clear")]
    public void Clear()
    {
        _mapPatch ??= GetSys<MapPatchSystem>();
        _mapPatch.Patchset.Clear();
    }

    [CommandImplementation("write")]
    public void Write([CommandArgument] ResPath path)
    {
        _mapPatch ??= GetSys<MapPatchSystem>();

        var text = _mapPatch.PatchToText();
        _res.UserData.WriteAllText(path, text);
    }
}

// This file is Part of CalDavSynchronizer (http://outlookcaldavsynchronizer.sourceforge.net/)
// Copyright (c) 2015 Gerhard Zehetbauer 
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using CalDavSynchronizer.Generic.EntityRelationManagement;
using CalDavSynchronizer.Generic.Synchronization;
using CalDavSynchronizer.Generic.Synchronization.States;
using CalDavSynchronizer.Implementation.ComWrappers;
using DDay.iCal;

namespace CalDavSynchronizer.Implementation.Tasks
{
  internal class TaskUpdateFromNewerToOlder
      : UpdateFromNewerToOlder<string, DateTime, TaskItemWrapper, Uri, string, IICalendar>
  {
    public TaskUpdateFromNewerToOlder (EntitySyncStateEnvironment<string, DateTime, TaskItemWrapper, Uri, string, IICalendar> environment, IEntityRelationData<string, DateTime, Uri, string> knownData, DateTime newA, string newB)
        : base (environment, knownData, newA, newB)
    {
    }

    protected override bool AIsNewerThanB
    {
      get
      {

        // Assume that no modification means, that the item is never modified. Therefore it must be new. 
        var todo = _bEntity.Todos[0];
        if (todo.LastModified == null)
          return false;

        return _aEntity.Inner.LastModificationTime.ToUniversalTime () >= todo.LastModified.UTC;
      }
    }
  }
}
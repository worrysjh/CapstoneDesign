// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mediapipe.Unity
{
#pragma warning disable IDE0065
  using Color = UnityEngine.Color;
#pragma warning restore IDE0065

  public sealed class ConnectionListAnnotation : ListAnnotation<ConnectionAnnotation>
  {
    [SerializeField] private Color _color = Color.white;
    [SerializeField, Range(0, 1)] private float _lineWidth = 1.0f;
    public List<int> wrongNumbers = new List<int> { 15, 16 };


#if UNITY_EDITOR
    private void OnValidate()
    {
      if (!UnityEditor.PrefabUtility.IsPartOfAnyPrefab(this))
      {
        ApplyColor(_color);
        ApplyLineWidth(_lineWidth);
      }
    }
#endif

    public void Fill(IList<(int, int)> connections, PointListAnnotation points)
    {
      Draw(connections.Select(pair => new Connection(points[pair.Item1], points[pair.Item2])).ToList());
    }

    public void SetColor(Color color)
    {
      _color = color;
      ApplyColor(color);
    }

    public void SetLineWidth(float lineWidth)
    {
      _lineWidth = lineWidth;
      ApplyLineWidth(lineWidth);
    }

    public void Draw(IList<Connection> targets)
    {
      if (ActivateFor(targets))
      {
        CallActionForAll(targets, (annotation, target) => { if (annotation != null) { annotation.Draw(target); } });
      }
    }

    public void Redraw()
    {
      int cnt = 0;
      foreach (var connection in children)
      {
        if (wrongNumbers.Contains(cnt)){
          if (connection != null) { connection.SetColor(Color.red); connection.Redraw(); }
        }
        else {
          if (connection != null) { connection.SetColor(Color.white); connection.Redraw(); }
        } 
        cnt++;
      }
    }

    protected override ConnectionAnnotation InstantiateChild(bool isActive = true)
    {
      var annotation = base.InstantiateChild(isActive);
      annotation.SetColor(_color);
      annotation.SetLineWidth(_lineWidth);
      return annotation;
    }

    private void ApplyColor(Color color)
    {
      foreach (var line in children)
      {
        if (line != null) { line.SetColor(color); }
      }
    }

    private void ApplyLineWidth(float lineWidth)
    {
      foreach (var line in children)
      {
        if (line != null) { line.SetLineWidth(lineWidth); }
      }
    }
  }
}

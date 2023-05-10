// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //
using Mediapipe.Unity;

namespace Mediapipe.Unity
{
  public class PoseWorldLandmarkListAnnotationController : AnnotationController<PoseLandmarkListAnnotation>
  {
    public IHierachicalAnnotation root; //
    private CorrectionScript cs;

    [SerializeField] private GameObject _correction;
    [SerializeField] private float _hipHeightMeter = 0.9f;
    [SerializeField] private Vector3 _scale = new Vector3(100, 100, 100);
    [SerializeField] private bool _visualizeZ = true;

    private IList<Landmark> _currentTarget;

    protected override void Start()
    {
      cs = _correction.GetComponent<CorrectionScript>();
      base.Start();
      transform.localPosition = new Vector3(0, _hipHeightMeter * _scale.y, 0);
    }

    public void DrawNow(IList<Landmark> target)
    {
      _currentTarget = target;
      SyncNow();
    }

    public void DrawNow(LandmarkList target)
    {
      // cs.target = target;
      // Debug.Log(target?.Landmark);
      DrawNow(target?.Landmark);
    }

    public void DrawLater(IList<Landmark> target)
    {
      UpdateCurrentTarget(target, ref _currentTarget);
    }

    public void DrawLater(LandmarkList target)
    {
      if (target != null){
        cs.target = target;
      }

      // Debug.Log(target?.Landmark);
      // Correct.Extract(target);
      // UnityEngine.Rect rect = root.transform.parent.gameObject.GetComponent<RectTransform>().rect;
      // Debug.Log(rect.GetPoint(target.Landmark, _scale, true)); // 
      // Debug.Log(rect.x); // 
      // PoseLandmarkListAnnotationController > PoseLandmarkListAnnotation > PointListAnnotation > PointAnnotation : HierarchicalAnnotation / Script Structure ::
      DrawLater(target?.Landmark);
    }

    protected override void SyncNow()
    {
      isStale = false;
      annotation.Draw(_currentTarget, _scale, _visualizeZ);
    }
  }
}

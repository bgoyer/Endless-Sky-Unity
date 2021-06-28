﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.OffScreenIndicator
{
    /// <summary>
    /// Attach the script to the off screen indicator panel.
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class OffScreenIndicator : MonoBehaviour
    {
        [Range(0.5f, 0.9f)]
        [Tooltip("Distance offset of the indicators from the centre of the screen")]
        [SerializeField] private float screenBoundOffset = 0.9f;

        public bool IsTargetVisible;
        private Camera mainCamera;
        private Vector3 screenCentre;
        private Vector3 screenBounds;

        private List<Target> targets = new List<Target>();

        public static Action<Target, bool> TargetStateChanged;

        private void Awake()
        {
            mainCamera = Camera.main;
            screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
            screenBounds = screenCentre * screenBoundOffset;
            TargetStateChanged += HandleTargetStateChanged;
        }

        private void LateUpdate()
        {
            DrawIndicators();
        }

        /// <summary>
        /// Draw the indicators on the screen and set thier position and rotation and other properties.
        /// </summary>
        public void DrawIndicators()
        {
            foreach (Target target in targets)
            {
                Vector3 screenPosition = OffScreenIndicatorCore.GetScreenPosition(mainCamera, target.transform.position);
                bool isTargetVisible = OffScreenIndicatorCore.IsTargetVisible(screenPosition);
                float distanceFromCamera = target.NeedDistanceText ? target.GetDistanceFromCamera(mainCamera.transform.position) : float.MinValue;// Gets the target distance from the camera.
                Indicator indicator = null;

                if (target.NeedBoxIndicator && isTargetVisible)
                {
                    screenPosition.z = 0;
                    indicator = GetIndicator(ref target.Indicator, IndicatorType.Box); // Gets the box indicator from the pool.
                }
                else if (target.NeedArrowIndicator && !isTargetVisible)
                {
                    float angle = float.MinValue;
                    OffScreenIndicatorCore.GetArrowIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
                    indicator = GetIndicator(ref target.Indicator, IndicatorType.Arrow); // Gets the arrow indicator from the pool.
                    indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.
                }
                else if (target.NeedStarIndicator && !isTargetVisible)
                {
                    float angle = float.MinValue;
                    OffScreenIndicatorCore.GetStarIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
                    indicator = GetIndicator(ref target.Indicator, IndicatorType.Star); // Gets the arrow indicator from the pool.
                    indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.
                }
                else if (target.NeedEnemyOutpostIndicator && !isTargetVisible)
                {
                    float angle = float.MinValue;
                    OffScreenIndicatorCore.GetEnemyOutpostIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
                    indicator = GetIndicator(ref target.Indicator, IndicatorType.Enemyoutpost); // Gets the arrow indicator from the pool.
                    indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.
                }
                else if (target.NeedPlanetIndicator && !isTargetVisible)
                {
                    float angle = float.MinValue;
                    OffScreenIndicatorCore.GetPlanetIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
                    indicator = GetIndicator(ref target.Indicator, IndicatorType.Planet); // Gets the arrow indicator from the pool.
                    indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.
                }
                else if (target.NeedAsteroidFieldIndicator && !isTargetVisible)
                {
                    float angle = float.MinValue;
                    OffScreenIndicatorCore.GetAsteroidFieldIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
                    indicator = GetIndicator(ref target.Indicator, IndicatorType.Star); // Gets the arrow indicator from the pool.
                    indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.
                }
                if (indicator)
                {
                    indicator.SetImageColor(target.TargetColor);// Sets the image color of the indicator.
                    indicator.SetDistanceText(distanceFromCamera, target); //Set the distance text for the indicator.
                    indicator.transform.position = screenPosition; //Sets the position of the indicator on the screen.
                    indicator.SetTextRotation(Quaternion.identity); // Sets the rotation of the distance text of the indicator.
                }
            }
        }

        /// <summary>
        /// 1. Add the target to targets list if <paramref name="active"/> is true.
        /// 2. If <paramref name="active"/> is false deactivate the targets indicator,
        ///     set its reference null and remove it from the targets list.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="active"></param>
        private void HandleTargetStateChanged(Target target, bool active)
        {
            if (active)
            {
                targets.Add(target);
            }
            else
            {
                target.Indicator?.Activate(false);
                target.Indicator = null;
                targets.Remove(target);
            }
        }

        /// <summary>
        /// Get the indicator for the target.
        /// 1. If its not null and of the same required <paramref name="type"/>
        ///     then return the same indicator;
        /// 2. If its not null but is of different type from <paramref name="type"/>
        ///     then deactivate the old reference so that it returns to the pool
        ///     and request one of another type from pool.
        /// 3. If its null then request one from the pool of <paramref name="type"/>.
        /// </summary>
        /// <param name="indicator"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Indicator GetIndicator(ref Indicator indicator, IndicatorType type)
        {
            if (indicator != null)
            {
                if (indicator.Type != type)
                {
                    indicator.Activate(false);

                    indicator = type == IndicatorType.Box ? BoxObjectPool.Current.GetPooledObject()

                        : ArrowObjectPool.Current.GetPooledObject();

                    indicator.Activate(true); // Sets the indicator as active.
                }
            }
            else
            {
                indicator = type == IndicatorType.Box ? BoxObjectPool.Current.GetPooledObject()

                    : type == IndicatorType.Star ? StarObjectPool.Current.GetPooledObject()

                    : type == IndicatorType.Enemyoutpost ? EnemyOutpostObjectPool.Current.GetPooledObject()

                    : type == IndicatorType.Outpost ? OutpostObjectPool.Current.GetPooledObject()

                    : type == IndicatorType.Planet ? PlanetObjectPool.Current.GetPooledObject()

                    : type == IndicatorType.Asteroidfield ? AsteroidFieldObjectPool.Current.GetPooledObject()

                    : ArrowObjectPool.Current.GetPooledObject();

                indicator.Activate(true); // Sets the indicator as active.
            }
            return indicator;
        }

        private void OnDestroy()
        {
            TargetStateChanged -= HandleTargetStateChanged;
        }
    }
}
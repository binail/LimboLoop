using UnityEngine;

namespace Features.Abstract.Utils.Cast
{
    public static class Raycaster
    {
        private static readonly Collider2D[] colliderResults = new Collider2D[1];
        private static readonly RaycastHit2D[] rayResults = new RaycastHit2D[1];

        public static bool CheckCircle(
            Vector2 _position,
            float _radius,
            int _layerMask)
        {
            int _resultsCount = Physics2D.OverlapCircleNonAlloc(_position, _radius, colliderResults, _layerMask);

            if (_resultsCount == 0)
                return false;

            return true;
        }

        public static Vector2 GetClosest(
            Vector2 _start,
            Vector2 _direction,
            float _distance,
            float _offset,
            int _layerMask)
        {
            int _resultsCount = Physics2D.RaycastNonAlloc(_start, _direction, rayResults, _distance, _layerMask);

            Vector2 _destination;

            if (_resultsCount == 0)
            {
                _destination = _start + _direction * _distance;

                Debug.DrawLine(_start, _destination, Color.green, 1f);

                return _destination;
            }

            _destination = Vector2.MoveTowards(_start, rayResults[0].point, _distance - _offset);
            Debug.DrawLine(_start, _destination, Color.red, 1f);
            return _destination;
        }

        public static Vector2 GetPoint(
            Vector2 _start,
            Vector2 _direction,
            int _layerMask,
            float _distance = 100f)
        {
            int _resultsCount = Physics2D.RaycastNonAlloc(
                _start,
                _direction,
                rayResults,
                _distance,
                _layerMask);

            if (_resultsCount == 0)
                return _start + _direction * _distance;

            return rayResults[0].point;
        }

        public static Vector2 GetPointOver(
            Vector2 _start,
            Vector2 _direction,
            int _layerMask,
            float _offset,
            float _distance = 100f)
        {
            int _resultsCount = Physics2D.RaycastNonAlloc(
                _start,
                _direction,
                rayResults,
                _distance,
                _layerMask);

            if (_resultsCount == 0)
                return _start;
            
            return rayResults[0].point - _direction * _offset;
        }

        public static bool IsObstaclesInLine(
            Vector2 _start,
            Vector2 _end,
            int _layerMask)
        {
            float _distance = Vector2.Distance(_start, _end);
            Vector2 _direction = _end - _start;
            _direction.Normalize();

            int _resultsCount = Physics2D.RaycastNonAlloc(
                _start,
                _direction,
                rayResults,
                _distance,
                _layerMask);

            if (_resultsCount == 0)
                return false;

            return true;
        }

        public static bool IsObstaclesInLine(
            Vector2 _start,
            Vector2 _direction,
            float _distance,
            int _layerMask)
        {
            _direction.Normalize();

            int _resultsCount = Physics2D.RaycastNonAlloc(
                _start,
                _direction,
                rayResults,
                _distance,
                _layerMask);

            if (_resultsCount == 0)
                return false;

            return true;
        }

        public static bool CheckPoint(
            Vector2 _start,
            int _layerMask)
        {
            int _resultsCount = Physics2D.OverlapPointNonAlloc(
                _start,
                colliderResults,
                _layerMask);

            if (_resultsCount == 0)
                return false;

            return true;
        }
    }
}
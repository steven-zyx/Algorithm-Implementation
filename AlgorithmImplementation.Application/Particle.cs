using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace AlgorithmImplementation.Application
{
    public class Particle
    {

        protected double _rx;
        protected double _ry;
        protected double _vx;
        protected double _vy;
        protected double _s;
        protected double _mass;
        public int Count { get; protected set; }

        public Particle()
        {

        }
        public Particle(double rx, double ry, double vx, double vy, double s, double mass)
        {
            _rx = rx;
            _ry = ry;
            _vx = vx;
            _vy = vy;
            _s = s;
            _mass = mass;
            Count = 0;
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public double TimeToHit(Particle otherParticle)
        {
            if (ReferenceEquals(this, otherParticle))
                return double.PositiveInfinity;

            double deltaPositionX = otherParticle._rx - _rx;
            double deltaPositionY = otherParticle._ry - _ry;

            double deltaVelocityX = otherParticle._vx - _vx;
            double deltaVelocityY = otherParticle._vy - _vy;

            double deltaPositionByDeltaVelocity = deltaPositionX * deltaVelocityX + deltaPositionY * deltaVelocityY;
            if (deltaPositionByDeltaVelocity > 0)
                return double.PositiveInfinity;

            double deltaVelocitySquared = deltaVelocityX * deltaVelocityX + deltaVelocityY * deltaVelocityY;
            double deltaPositionSquared = deltaPositionX * deltaPositionX + deltaPositionY * deltaPositionY;

            double distanceBetweenCenters = _s + otherParticle._s;
            double distanceBetweenCentersSquared = distanceBetweenCenters * distanceBetweenCenters;

            // Check if particles overlap
            if (deltaPositionSquared < distanceBetweenCentersSquared)
                throw new Exception("Invalid state: overlapping particles. No two objects can occupy the same space " +
                        "at the same time.");

            double distance = (deltaPositionByDeltaVelocity * deltaPositionByDeltaVelocity)
                    - deltaVelocitySquared * (deltaPositionSquared - distanceBetweenCentersSquared);

            if (distance < 0)
                return double.PositiveInfinity;

            return -(deltaPositionByDeltaVelocity + Math.Sqrt(distance)) / deltaVelocitySquared;
        }

        public double TimeToHitHorizontalWall()
        {
            double distance;
            if (_vy < 0)
                distance = _s - _ry;
            else if (_vy > 0)
                distance = 1 - _s - _ry;
            else
                return double.PositiveInfinity;
            return distance / _vy;
        }

        public double TimeToHitVerticalWall()
        {
            double distance;
            if (_vx < 0)
                distance = _s - _rx;
            else if (_vx > 0)
                distance = 1 - _s - _rx;
            else
                return double.PositiveInfinity;
            return distance / _vx;
        }

        public void BounceOff(Particle otherParticle)
        {
            double deltaPositionX = otherParticle._rx - _rx;
            double deltaPositionY = otherParticle._ry - _ry;

            double deltaVelocityX = otherParticle._vx - _vx;
            double deltaVelocityY = otherParticle._vy - _vy;

            double deltaPositionByDeltaVelocity = deltaPositionX * deltaVelocityX + deltaPositionY * deltaVelocityY;

            double distanceBetweenCenters = _s + otherParticle._s;

            // Compute normal force
            double magnitudeOfNormalForce = 2 * _mass * otherParticle._mass * deltaPositionByDeltaVelocity
                    / ((_mass + otherParticle._mass) * distanceBetweenCenters);

            double normalForceInXDirection = magnitudeOfNormalForce * deltaPositionX / distanceBetweenCenters;
            double normalForceInYDirection = magnitudeOfNormalForce * deltaPositionY / distanceBetweenCenters;

            // Update velocities according to the normal force
            _vx += normalForceInXDirection / _mass;
            _vy += normalForceInYDirection / _mass;
            otherParticle._vx -= normalForceInXDirection / otherParticle._mass;
            otherParticle._vy -= normalForceInYDirection / otherParticle._mass;

            // Update collision counts
            Count++;
            otherParticle.Count++;
        }

        public void BounceOffHorizontalWall()
        {
            _vy = -_vy;
            Count++;
        }

        public void BounceOffVerticalWall()
        {
            _vx = -_vx;
            Count++;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using OrderServer31.Models;

namespace OrderServer31.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderlinesController : ControllerBase {
        private readonly OrderDbContext _context;

        public OrderlinesController(OrderDbContext context) {
            _context = context;
        }

        // GET: api/Orderlines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orderline>>> GetOrderlines() {
            return await _context.Orderlines.ToListAsync();
        }

        // GET: api/Orderlines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orderline>> GetOrderline(int id) {
            var orderline = await _context.Orderlines.FindAsync(id);

            if(orderline == null) {
                return NotFound();
            }

            return orderline;
        }

        // PUT: api/Orderlines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderline(int id, Orderline orderline) {
            if(id != orderline.Id) {
                return BadRequest();
            }

            _context.Entry(orderline).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!OrderlineExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orderlines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Orderline>> PostOrderline(Orderline orderline) {
            _context.Orderlines.Add(orderline);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderline", new { id = orderline.Id }, orderline);
        }

        // DELETE: api/Orderlines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Orderline>> DeleteOrderline(int id) {
            var orderline = await _context.Orderlines.FindAsync(id);
            if(orderline == null) {
                return NotFound();
            }

            _context.Orderlines.Remove(orderline);
            await _context.SaveChangesAsync();

            return orderline;
        }

        private bool OrderlineExists(int id) {
            return _context.Orderlines.Any(e => e.Id == id);
        }
    }
}

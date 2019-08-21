#define XX_PREDECLS

#define XX_INFO \
            "pcg_oneseq_32_rxs_m_xs_32_random_r:\n" \
            "      -  result:      32-bit unsigned int (uint32_t)\n" \
            "      -  period:      2^32\n" \
            "      -  state type:  struct pcg_state_32 (%zu bytes)\n" \
            "      -  output func: RXS-M-XS\n" \
            "\n", sizeof(struct pcg_state_32)

#define XX_NUMBITS                  "  32bit:"
#define XX_NUMVALUES                6
#define XX_NUMWRAP                  6
#define XX_PRINT_RNGVAL(value)      printf(" 0x%08x", value)
#define XX_RAND_DECL                struct pcg_state_32 rng;
#define XX_SEEDSDECL(seeds)         uint32_t seeds[1];
#define XX_SRANDOM_SEEDARGS(seeds)  seeds[0]
#define XX_SRANDOM_SEEDCONSTS       4294967295u
#define XX_SRANDOM(...)             \
            pcg_oneseq_32_srandom_r(&rng, __VA_ARGS__)
#define XX_RANDOM()                 \
            pcg_oneseq_32_rxs_m_xs_32_random_r(&rng)
#define XX_BOUNDEDRAND(bound)       \
            pcg_oneseq_32_rxs_m_xs_32_boundedrand_r(&rng, bound)
#define XX_ADVANCE(delta)           \
            pcg_oneseq_32_advance_r(&rng, delta)

#include "pcg_variants.h"
#include "check-base.c"
